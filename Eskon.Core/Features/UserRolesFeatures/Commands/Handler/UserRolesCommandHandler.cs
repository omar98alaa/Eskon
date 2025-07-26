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
        public async Task<Response<TokenResponseDto>> Handle(AddOwnerRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserToBeOwnerId.ToString());

            if (user == null)
                return NotFound<TokenResponseDto>("User Not Found");

            var result = await _userManager.AddToRoleAsync(user, "Owner");

            if (!result.Succeeded)
            {
                var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
                return BadRequest<TokenResponseDto>(dbErrorMessages);
            }

            var userManagerClaims = await _userManager.GetClaimsAsync(user);
            var userManagerRoles = await _userManager.GetRolesAsync(user);

            var newAccessToken = _serviceUnitOfWork.AuthenticationService.GenerateJWTTokenAsync(user, userManagerRoles, userManagerClaims);

            var existingRefreshToken = await _serviceUnitOfWork.RefreshTokenService.GetTokenByUserIdAsync(user.Id);

            var tokenResponse = new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = existingRefreshToken.RefreshToken // reuse the existing one
            };

            return Success(tokenResponse, "User owner role added, new access token issued.");
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
