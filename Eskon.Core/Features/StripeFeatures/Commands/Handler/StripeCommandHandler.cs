using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;
using Stripe.Checkout;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Core.Features.StripeFeatures.Commands.Handler
{
    public class StripeCommandHandler : ResponseHandler, IStripeCommandHandler
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        #endregion

        #region Constructors
        public StripeCommandHandler(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }
        #endregion

        #region stripe account handlers
        public async Task<Response<string>> Handle(CreateStripeAccountCommand request, CancellationToken cancellationToken)
        {
            User user = await _serviceUnitOfWork.UserService.GetUserByIdAsync(request.userId);
            if (user == null)
            {
                return NotFound<string>("User not found");
            }

            if (user.stripeAccountId != null || user?.stripeAccountId?.Trim() == string.Empty)
            {
                return BadRequest<string>("User already has an active stripe account");
            }

            string AccountId = _serviceUnitOfWork.StripeService.CreateStripeAccountForOwner(user);

            if (string.IsNullOrEmpty(AccountId))
            {
                throw new Exception("Error creating a stripe account");
            }

            return Created<string>(AccountId);
        }

        public async Task<Response<string>> Handle(CreateStripeConnectedAccountLinkForOwnerCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.RequestDTO);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.RequestDTO, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<string>(internalErrorMessages);
            }


            string connectedAccountFillLink = _serviceUnitOfWork.StripeService.CreateStripeConnectedAccountLinkForOwner(
               stripeAccountId: request.RequestDTO.stripeAccountId,
               refreshUrl: request.RequestDTO.refreshUrl,
               returnUrl: request.RequestDTO.returnUrl);

            if (string.IsNullOrEmpty(connectedAccountFillLink))
            {
                throw new Exception("Error creating a stripe account fill link");
            }

            return Success(connectedAccountFillLink);
        }
        #endregion

        #region Stripe Checkout
        public async Task<Response<string>> Handle(CreateStripeCheckoutLinkCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.RequestDTO);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.RequestDTO, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<string>(internalErrorMessages);
            }

            Booking userBooking = await _serviceUnitOfWork.BookingService.GetBookingById(request.bookingId);

            if (userBooking == null)
            {
                return NotFound<string>("Booking does not exists");
            }

            if (userBooking.CustomerId != request.customerId)
            {
                return Forbidden<string>();
            }

            if (!userBooking.IsAccepted)
            {
                return BadRequest<string>("Booking is not accepted from owner");
            }

            Session checkoutSession = _serviceUnitOfWork.StripeService.CreateStripeCheckoutSession(
                booking: userBooking,
                successUrl: request.RequestDTO.SuccessUrl,
                cancelUrl: request.RequestDTO.CancelUrl);

            if (checkoutSession == null || string.IsNullOrEmpty(checkoutSession.Url))
            {
                throw new Exception("Can not create checkout session");
            }

            var payment = new Payment()
            {
                BookingAmount = userBooking.TotalPrice,
                Fees = _serviceUnitOfWork.StripeService.CalculateEskonBookingFees(userBooking.TotalPrice),
                MaximumRefundDate = userBooking.StartDate.AddDays(-1),
                //StripeChargeId = userBooking.Id.ToString(),
                CustomerId = userBooking.CustomerId,
                BookingId = userBooking.Id,
            };
            await _serviceUnitOfWork.PaymentService.AddPaymentAsync(payment);
            await _serviceUnitOfWork.SaveChangesAsync();

            return Success(checkoutSession.Url);
        }
        #endregion

        #region Stripe Refund
        public async Task<Response<string>> Handle(CreateStripePaymentRefundCommand request, CancellationToken cancellationToken)
        {
            var booking = await _serviceUnitOfWork.BookingService.GetBookingById(request.BookingId);
            
            if (booking == null)
            {
                return NotFound<string>("Booking does not exist");
            }

            if (booking.Payment == null)
            {
                return NotFound<string>("Payment does not exist");
            }

            if (booking.CustomerId != request.CustomerId)
            {
                return Forbidden<string>();
            }

            if (!booking.IsPayed)
            {
                return BadRequest<string>("Booking is not payed");
            }

            var today = DateOnly.FromDateTime(DateTime.Now);

            if (booking.Payment.MaximumRefundDate < today)
            {
                return BadRequest<string>("Booking cannot be refunded, maximum refund date passed");
            }

            if (booking.Payment.IsRefund)
            {
                return BadRequest<string>("Payment already refunded");
            }

            _serviceUnitOfWork.StripeService.CreateStripeRefund(booking.Payment.StripeChargeId);
            return Success("Refund request is created...");
        }
        #endregion
    }
}
