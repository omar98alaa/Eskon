using Eskon.API.Base;
using Eskon.Core.Features.NotificationFeatures.Commands.Command;
using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Core.Features.UserRolesFeatures.Commands.Command;
using Eskon.Core.Response;
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
        #region Stripe Webhook
        /// <summary>
        /// Handles incoming Stripe webhook events for payment and account lifecycle.
        /// </summary>
        /// <remarks>
        /// This endpoint processes Stripe events to update the system's payment and booking statuses,
        /// and to handle connected account lifecycle events.
        /// 
        /// **Supported Stripe Event Types:**
        /// 
        /// - <c>checkout.session.completed</c>: Triggered when a customer completes the checkout.
        /// - <c>checkout.session.async_payment_succeeded</c>: Triggered when an asynchronous payment method succeeds.
        /// - <c>checkout.session.async_payment_failed</c>: Triggered when an asynchronous payment method fails.
        /// - <c>account.external_account.created</c>: Triggered when a connected account is created.
        /// - <c>charge.refunded</c>: Triggered when a charge is fully refunded.
        /// - <c>payment_intent.succeeded</c>: Triggered when a synchronous payment succeeds.
        /// - <c>payment_intent.payment_failed</c>: Triggered when a synchronous payment fails.
        /// 
        /// **Workflow Highlights:**
        /// - Updates payment status in the database (Success, Failed, Refunded).
        /// - Updates booking status (Paid, Soft Removed).
        /// - Assigns the "Owner" role when a Stripe connected account is created.
        /// - Logs unhandled events for review.
        /// 
        /// **Security:**
        /// - Validates the webhook signature using the Stripe endpoint secret.
        /// 
        /// **Example Use Cases:**
        /// - Confirming bookings after payment.
        /// - Automatically downgrading bookings when a payment fails.
        /// - Handling refunds by removing associated bookings.
        /// - Assigning property owner role upon connected account creation.
        /// </remarks>
        /// <returns>
        /// 200 OK if the event is successfully processed;  
        /// 400 Bad Request if a Stripe signature or request validation fails;  
        /// 500 Internal Server Error for unhandled exceptions.
        /// </returns>
        /// <response code="200">Event successfully received and processed.</response>
        /// <response code="400">Invalid Stripe webhook signature or payload.</response>
        /// <response code="500">Unhandled exception occurred while processing the event.</response>
        [HttpPost("webhook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

                                // Notification - Payment Success (Async)
                                await Mediator.Send(new SendNotificationCommand(
                                    ReceiverId: payment.Booking.CustomerId,
                                    Content: $"Your payment for booking '{payment.Booking.Property.Title}' was successful.",
                                    NotificationTypeName: "Payment Success",
                                    RedirectionId: payment.Booking.Id,
                                    RedirectionName: "BookingDetails"
                                ));
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

                                //Notification - Payment Failed (Async)
                                await Mediator.Send(new SendNotificationCommand(
                                    ReceiverId: payment.Booking.CustomerId,
                                    Content: $"Your payment for booking '{payment.Booking.Property.Title}' has failed.",
                                    NotificationTypeName: "Payment Failed",
                                    RedirectionId: payment.Booking.Id,
                                    RedirectionName: "BookingDetails"
                                ));
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


                                    // Notification - Refund Issued
                                    await Mediator.Send(new SendNotificationCommand(
                                        ReceiverId: payment.Booking.CustomerId,
                                        Content: $"A refund has been issued for booking '{payment.Booking.Property.Title}'.",
                                        NotificationTypeName: "Refund Issued",
                                        RedirectionId: payment.Booking.Id,
                                        RedirectionName: "BookingDetails"
                                    ));
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


                                //Notification - Payment Success (Sync)
                                await Mediator.Send(new SendNotificationCommand(
                                    ReceiverId: payment.Booking.CustomerId,
                                    Content: $"Your payment for booking '{payment.Booking.Property.Title}' was successful.",
                                    NotificationTypeName: "Payment Success",
                                    RedirectionId: payment.Booking.Id,
                                    RedirectionName: "BookingDetails"
                                ));
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

                                // Notification - Payment Failed (Sync)
                                await Mediator.Send(new SendNotificationCommand(
                                    ReceiverId: payment.Booking.CustomerId,
                                    Content: $"Your payment for booking '{payment.Booking.Property.Title}' has failed.",
                                    NotificationTypeName: "Payment Failed",
                                    RedirectionId: payment.Booking.Id,
                                    RedirectionName: "BookingDetails"
                                ));
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
        #endregion

        #region request connected account fill link
        /// <summary>
        /// Generates a Stripe connected account fill link for a property owner.
        /// </summary>
        /// <remarks>
        /// This endpoint is used to create a Stripe onboarding link for a property owner
        /// so they can complete their connected account setup (e.g., providing banking details, identity verification).
        /// 
        /// **Workflow:**
        /// 1. Validates the request payload (<see cref="CreateStripeConnectedAccountFillLinkRequestDTO"/>).
        /// 2. Calls the Stripe service to generate a connected account onboarding link.
        /// 3. Returns the link if successful, or an error if validation or creation fails.
        /// 
        /// **Security:**
        /// - Requires authentication via <c>[Authorize]</c> attribute.
        /// - Intended for owners or admins managing Stripe payouts.
        /// 
        /// **Example Request Body:**
        /// ```json
        /// {
        ///   "stripeAccountId": "acct_123456789",
        ///   "refreshUrl": "https://example.com/stripe/refresh",
        ///   "returnUrl": "https://example.com/stripe/return"
        /// }
        /// ```
        /// 
        /// **Example Response:**
        /// ```json
        /// {
        ///   "success": true,
        ///   "data": "https://connect.stripe.com/setup/s/acct_123456789/abc123",
        ///   "errors": []
        /// }
        /// ```
        /// </remarks>
        /// <param name="requestDTO">
        /// The DTO containing the Stripe connected account ID, refresh URL, and return URL
        /// used to create the onboarding link.
        /// </param>
        /// <returns>
        /// A response containing the Stripe onboarding link as a string if successful.
        /// </returns>
        /// <response code="200">Successfully generated the Stripe connected account fill link.</response>
        /// <response code="400">Validation failed or the request contains invalid parameters.</response>
        /// <response code="401">Unauthorized request. Authentication is required.</response>
        /// <response code="500">An error occurred while creating the connected account fill link.</response>
        [Authorize]
        [HttpPost("request-connected-account-fill-link")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateConnectedAccountFillLink([FromBody] CreateStripeConnectedAccountFillLinkRequestDTO requestDTO)
        {
            return NewResult(await Mediator.Send(new CreateStripeConnectedAccountLinkForOwnerCommand(requestDTO)));
        }
        #endregion

        #region Delete Stripe connected account
        /// <summary>
        /// Deletes a Stripe connected account.
        /// </summary>
        /// <remarks>
        /// This endpoint is used by administrators to permanently delete a Stripe connected account
        /// associated with a user or business.  
        /// 
        /// **Workflow:**
        /// 1. Requires **Admin** authorization.
        /// 2. Uses the configured Stripe secret key to authenticate the request with Stripe.
        /// 3. Calls Stripe’s <see cref="AccountService.Delete(string, RequestOptions)"/> method to delete the connected account.
        /// 4. Returns the deleted account object as confirmation.
        /// 
        /// **Important Notes:**
        /// - Deleting a Stripe connected account is **irreversible**.
        /// - All related payouts, charges, and account data will be removed according to Stripe’s retention policies.
        /// - This operation should only be performed when an account is no longer needed.
        /// 
        /// **Example Request:**
        /// ```http
        /// DELETE /Delete?stripeAccountId=acct_123456789
        /// Authorization: Bearer {admin_jwt_token}
        /// ```
        /// 
        /// **Example Successful Response:**
        /// ```json
        /// {
        ///   "id": "acct_123456789",
        ///   "object": "account",
        ///   "deleted": true
        /// }
        /// ```
        /// </remarks>
        /// <param name="stripeAccountId">
        /// The unique identifier of the Stripe connected account to delete (e.g., <c>acct_123456789</c>).
        /// </param>
        /// <returns>
        /// The deleted Stripe account object returned by Stripe’s API.
        /// </returns>
        /// <response code="200">The Stripe connected account was successfully deleted.</response>
        /// <response code="400">The <paramref name="stripeAccountId"/> is missing or invalid.</response>
        /// <response code="401">Unauthorized request — authentication required.</response>
        /// <response code="403">Forbidden — only admins can perform this operation.</response>
        /// <response code="500">An internal server error occurred while communicating with Stripe.</response>
        [Authorize("Admin")]
        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteStripeAccount(string stripeAccountId)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var service = new AccountService();
            Account deleted = service.Delete(stripeAccountId);
            return Ok(deleted);
        } 
        #endregion
        #endregion

    }
}