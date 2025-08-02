using Eskon.API.Base;
using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Core.Features.UserRolesFeatures.Commands.Command;
using Eskon.Domian.DTOs.StripeDTOs;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Stripe;
using Eskon.Service.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
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
                var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, endpointSecret, throwOnApiVersionMismatch: false);

                if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                    var intent = stripeEvent.Data.Object as PaymentIntent;

                    var payment = _unitOfWork.PaymentService.GetPaymentByChargedId(intent.LatestChargeId);

                    if (payment != null)
                    {
                        await _unitOfWork.PaymentService.SetPaymentAsSuccess(payment);

                        await _unitOfWork.BookingService.SetBookingAsPayedAsync(payment.Booking);
                        
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
                else if (stripeEvent.Type == EventTypes.CheckoutSessionAsyncPaymentFailed)
                {
                    var intent = stripeEvent.Data.Object as PaymentIntent;

                    var payment = _unitOfWork.PaymentService.GetPaymentByChargedId(intent.LatestChargeId);

                    if (payment != null)
                    {
                        await _unitOfWork.PaymentService.SetPaymentAsFailed(payment);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
                else if (stripeEvent.Type == EventTypes.AccountExternalAccountCreated) // fires when a new account is created
                {
                    var connectedAccountId = stripeEvent.Account;
                    _logger.LogInformation($"---Account {connectedAccountId} Added To Stripe---");

                    // Add Owner Role and generate the new access token
                    User user = await _unitOfWork.UserService.GetUserByStripeAccountIdAsync(connectedAccountId);
                    await Mediator.Send(new AddOwnerRoleToUserCommand(user));
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(new { error = e.Message });
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