using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.UnitOfWork;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Core.Features.StripeFeatures.Commands.Handler
{
    public class StripeCommandHandler : ResponseHandler, 
                                    IRequestHandler<CreateStripeAccountCommand, Response<string>>,
                                    IRequestHandler<CreateStripeConnectedAccountLinkForOwnerCommand, Response<string>>,
                                    IRequestHandler<CreateStripeCheckoutLinkCommand, Response<string>>
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

        #region strip account handlers
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

            if(string.IsNullOrEmpty(AccountId))
            {
                return BadRequest<string>("Error creating a stripe account");
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
                return BadRequest<string>("Error creating a stripe account fill link");
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

            string checkoutSessionLink = _serviceUnitOfWork.StripeService.CreateStripeCheckoutUrl(
                booking: request.RequestDTO.Booking,
                successUrl: request.RequestDTO.SuccessUrl, 
                cancelUrl: request.RequestDTO.CancelUrl);

            if (string.IsNullOrEmpty(checkoutSessionLink))
            {
                return BadRequest<string>("Can not create checkout session");
            }

            return Success(checkoutSessionLink);
        }
        #endregion


    }
}
