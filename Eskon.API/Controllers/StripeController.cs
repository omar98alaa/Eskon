using Eskon.API.Base;
using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Core.Features.UserRolesFeatures.Commands.Command;
using Eskon.Domian.DTOs.StripeDTOs;
using Eskon.Domian.Stripe;
using Eskon.Service.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Text;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : BaseController
    {
        #region Fields
        private readonly StripeSettings _stripeSettings;
        private readonly IServiceUnitOfWork _unitOfWork;
        private readonly ILogger<StripeController> _logger;
        #endregion

        #region Constructors
        public StripeController(StripeSettings stripeSettings, IServiceUnitOfWork unitOfWork, ILogger<StripeController> logger)
        {
            _stripeSettings = stripeSettings;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion

        #region Endpoints

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body, Encoding.UTF8).ReadToEndAsync();
            var stripeSignature = Request.Headers["Stripe-Signature"];
            var endpointSecret = _stripeSettings.WebhookSecret;

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json, stripeSignature, endpointSecret, throwOnApiVersionMismatch: false);

                switch (stripeEvent.Type)
                {

                    case EventTypes.CheckoutSessionCompleted:
                        {
                            //
                            //  Customer completes the payment form
                            //

                            //var session = stripeEvent.Data.Object as Session;
                            //if (session == null) break;
                            //var payment = _unitOfWork.PaymentService.GetPaymentByChargedId(session.Id);

                            break;
                        }
                    case EventTypes.CheckoutSessionAsyncPaymentSucceeded:
                        {
                            //
                            //  Payment with an async method succeeds
                            //

                            var session = stripeEvent.Data.Object as Session;
                            if (session == null) break;
                            var payment = await _unitOfWork.PaymentService.GetPaymentByBookingIdAsync(Guid.Parse(session.PaymentIntent.Metadata["BookingId"]));
                            if (payment != null)
                            {
                                payment.StripeChargeId = session.PaymentIntent.LatestChargeId;
                                await _unitOfWork.PaymentService.SetPaymentAsSuccess(payment);
                                await _unitOfWork.BookingService.SetBookingAsPayedAsync(payment.Booking);
                                await _unitOfWork.SaveChangesAsync();
                            }

                            break;
                        }
                    case EventTypes.CheckoutSessionAsyncPaymentFailed:
                        {
                            //
                            //  Payment with an async method fails
                            //

                            var session = stripeEvent.Data.Object as Session;
                            if (session == null) break;
                            var payment = await _unitOfWork.PaymentService.GetPaymentByBookingIdAsync(Guid.Parse(session.PaymentIntent.Metadata["BookingId"]));
                            if (payment != null)
                            {
                                payment.StripeChargeId = session.PaymentIntent.LatestChargeId;
                                await _unitOfWork.PaymentService.SetPaymentAsFailed(payment);
                                await _unitOfWork.SaveChangesAsync();
                            }

                            break;
                        }
                    case EventTypes.AccountExternalAccountCreated:
                        {
                            //
                            //  Connected account created
                            //

                            var connectedAccountId = stripeEvent.Account;
                            _logger.LogInformation($"--- Stripe Connected Account Created: {connectedAccountId} ---");

                            var user = await _unitOfWork.UserService.GetUserByStripeAccountIdAsync(connectedAccountId);
                            if (user != null)
                            {
                                await Mediator.Send(new AddOwnerRoleToUserCommand(user));
                            }

                            break;
                        }
                    case EventTypes.ChargeRefunded:
                        {
                            //
                            //  Charge successfully refunded
                            //

                            var charge = stripeEvent.Data.Object as Charge;
                            if (charge == null) break;
                            if (charge.AmountRefunded == charge.Amount - charge.ApplicationFeeAmount)
                            {
                                var payment = await _unitOfWork.PaymentService.GetPaymentByChargedId(charge.Id);
                                if (payment != null)
                                {
                                    await _unitOfWork.PaymentService.SetPaymentAsRefunded(payment);
                                    await _unitOfWork.BookingService.SoftRemoveBookingAsync(payment.Booking);
                                    await _unitOfWork.SaveChangesAsync();
                                }
                            }


                            break;
                        }
                    case EventTypes.PaymentIntentSucceeded:
                        {
                            //
                            //  Payment with a sync method succeeds
                            //

                            var intent = stripeEvent.Data.Object as PaymentIntent;
                            if (intent == null) break;
                            var payment = await _unitOfWork.PaymentService.GetPaymentByBookingIdAsync(Guid.Parse(intent.Metadata["BookingId"]));
                            if (payment != null)
                            {
                                payment.StripeChargeId = intent.LatestChargeId;
                                await _unitOfWork.PaymentService.SetPaymentAsSuccess(payment);
                                await _unitOfWork.BookingService.SetBookingAsPayedAsync(payment.Booking);
                                await _unitOfWork.SaveChangesAsync();
                            }
                            break;
                        }
                    case EventTypes.PaymentIntentPaymentFailed:
                        {
                            //
                            //  Payment with a sync method fails
                            //

                            var intent = stripeEvent.Data.Object as PaymentIntent;
                            if (intent == null) break;
                            var payment = await _unitOfWork.PaymentService.GetPaymentByBookingIdAsync(Guid.Parse(intent.Metadata["BookingId"]));
                            if (payment != null)
                            {
                                payment.StripeChargeId = intent.LatestChargeId;
                                await _unitOfWork.PaymentService.SetPaymentAsFailed(payment);
                                await _unitOfWork.SaveChangesAsync();
                            }
                            break;
                        }

                    default:
                        _logger.LogInformation($"Unhandled Stripe Event: {stripeEvent.Type}");
                        break;
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "StripeException in webhook.");
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in Stripe webhook.");
                return StatusCode(500, new { error = ex.Message });
            }
        }


        [Authorize]
        [HttpPost("request-connected-account-fill-link")]
        public async Task<IActionResult> CreateConnectedAccountFillLink([FromBody] CreateStripeConnectedAccountFillLinkRequestDTO requestDTO)
        {
            return NewResult(await Mediator.Send(new CreateStripeConnectedAccountLinkForOwnerCommand(requestDTO)));
        }

        [Authorize("Admin")]
        [HttpDelete("Delete")]
        public IActionResult DeleteStripeAccount(string stripeAccountId)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var service = new AccountService();
            Account deleted = service.Delete(stripeAccountId);
            return Ok(deleted);
        }

        #endregion

    }
}