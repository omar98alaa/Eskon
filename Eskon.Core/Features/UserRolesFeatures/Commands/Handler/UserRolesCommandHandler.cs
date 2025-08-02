using Eskon.Core.Features.UserRolesFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Eskon.Core.Features.UserRolesFeatures.Commands.Handler
{
    public class UserRolesCommandHandler : ResponseHandler, IUserRolesCommandHandler

    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserRolesCommandHandler(IServiceUnitOfWork serviceUnitOfWork, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _userManager = userManager;
        }
        #endregion

        #region Add Owner Role

        // Add Stripe Account
        public async Task<Response<string>> Handle(CreateStripeConnectedAccountAndFillLinkCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.OwnerRoleDTO);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.OwnerRoleDTO, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<string>(internalErrorMessages);
            }

            var user = await _userManager.FindByIdAsync(request.UserToBeOwnerId.ToString());

            if (user == null)
                return NotFound<string>("User Not Found");

            if (!string.IsNullOrEmpty(user.stripeAccountId))
            {
                return BadRequest<string>("User already has an active stripe account");
            }
            var stripeAccountId = _serviceUnitOfWork.StripeService.CreateStripeAccountForOwner(user);

            // Needs to be more precise
            if (string.IsNullOrEmpty(stripeAccountId))
            {
                return BadRequest<string>("Error at stripe");
            }
            await _serviceUnitOfWork.UserService.SetUserStripeAccountIdAsync(user.Id, stripeAccountId);
            await _serviceUnitOfWork.SaveChangesAsync();

            string connectedAccountFillLink = _serviceUnitOfWork.StripeService
                .CreateStripeConnectedAccountLinkForOwner(
                stripeAccountId, 
                request.OwnerRoleDTO.RefreshUrl, 
                request.OwnerRoleDTO.ReturnUrl);

            if (string.IsNullOrEmpty(connectedAccountFillLink))
            {
                return BadRequest<string>("Error creating a stripe account fill link");
            }

            return Success(connectedAccountFillLink);
        }

        // Add OwnerRole To database
        public async Task<Response<string>> Handle(AddOwnerRoleToUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserToBeOwner == null)
                return NotFound<string>("User Not Found");

            var result = await _userManager.AddToRoleAsync(request.UserToBeOwner, "Owner");

            if (!result.Succeeded)
            {
                var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
                return BadRequest<string>(dbErrorMessages);
            }

            var userManagerClaims = await _userManager.GetClaimsAsync(request.UserToBeOwner);
            var userManagerRoles = await _userManager.GetRolesAsync(request.UserToBeOwner);

            var newAccessToken = _serviceUnitOfWork.AuthenticationService.GenerateJWTTokenAsync(request.UserToBeOwner, userManagerRoles, userManagerClaims);

            var existingRefreshToken = await _serviceUnitOfWork.RefreshTokenService.GetTokenByUserIdAsync(request.UserToBeOwner.Id);

            var tokenResponse = new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = existingRefreshToken.RefreshToken // reuse the existing one
            };

            return Success("User owner role added, new access token issued.");
        }

        #endregion

        #region Add Admin Role
        public async Task<Response<string>> Handle(AddAdminRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.AdminRole.Email);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.AdminRole.Email, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<string?>(internalErrorMessages);
            }
            var user = await _userManager.FindByEmailAsync(request.AdminRole.Email);
            if (user == null)
                return NotFound<string>("User Not Found");

            var result = await _userManager.AddToRoleAsync(user, "Admin");

            if (!result.Succeeded)
            {
                var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
                return BadRequest<string>(dbErrorMessages);
            }

            return Success("User admin role added, new access token issued.");
        }
        #endregion

        #region Delete Admin Role
        public async Task<Response<string>> Handle(DeleteAdminRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.AdminRole.Email);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.AdminRole.Email, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<string?>(internalErrorMessages);
            }

            var user = await _userManager.FindByEmailAsync(request.AdminRole.Email);

            if (user == null)
                return NotFound<string>("User Not Found");

            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");

            if (!result.Succeeded)
            {
                var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
                return BadRequest<string>(dbErrorMessages);
            }
            return Success("User admin role deleted, new access token issued.");
        }
        #endregion
    }
}
