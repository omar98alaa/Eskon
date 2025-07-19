using AutoMapper;
using Eskon.Core.Features.UserRolesFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Eskon.Core.Features.UserRolesFeatures.Commands.Handler
{
    public class UserRolesCommandHandler : ResponseHandler
        , IRequestHandler<AddOwnerRoleToUserCommand, Response<TokenResponseDto>>
        , IRequestHandler<DeleteOwnerRoleFromUserCommand, Response<TokenResponseDto>>
        , IRequestHandler<AddAdminRoleToUserCommand, Response<string>>
        , IRequestHandler<DeleteAdminRoleFromUserCommand, Response<string>>
    {
        #region Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly SignInManager<User> _signInManager;
        #endregion

        #region Constructors
        public UserRolesCommandHandler(IAuthenticationService authenticationService, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, SignInManager<User> signInManager, IRefreshTokenService refreshTokenService)
        {
            _authenticationService = authenticationService;
            _refreshTokenService = refreshTokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Add Owner Role
        public async Task<Response<TokenResponseDto>> Handle(AddOwnerRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserToBeOwnerId.ToString());

            if (user == null)
                return BadRequest<TokenResponseDto>("User Not Found");

            var result = await _userManager.AddToRoleAsync(user, "Owner");

            if (!result.Succeeded)
            {
                var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
                return BadRequest<TokenResponseDto>(dbErrorMessages);
            }

            var newAccessToken = await _authenticationService.GenerateJWTTokenAsync(user);

            var existingRefreshToken = await _refreshTokenService.GetTokenByUserIdAsync(user.Id);

            if (existingRefreshToken == null || existingRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                return BadRequest<TokenResponseDto>("Refresh token not found or expired. Please log in again.");
            }

            var tokenResponse = new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = existingRefreshToken.RefreshToken // reuse the existing one
            };

            return Success(tokenResponse, "User owner role added, new access token issued.");
        }

        #endregion

        #region Delete Owner Role
        public async Task<Response<TokenResponseDto>> Handle(DeleteOwnerRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserToRemoveOwnerId.ToString());

            if (user == null)
                return BadRequest<TokenResponseDto>("User Not Found");

            var result = await _userManager.RemoveFromRoleAsync(user, "Owner");

            if (!result.Succeeded)
            {
                var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
                return BadRequest<TokenResponseDto>(dbErrorMessages);
            }

            var newAccessToken = await _authenticationService.GenerateJWTTokenAsync(user);

            var existingRefreshToken = await _refreshTokenService.GetTokenByUserIdAsync(user.Id);

            if (existingRefreshToken == null || existingRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                return BadRequest<TokenResponseDto>("Refresh token not found or expired. Please log in again.");
            }

            var tokenResponse = new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = existingRefreshToken.RefreshToken // reuse the existing one
            };

            return Success(tokenResponse, "User owner role deleted, new access token issued.");
        }
        #endregion

        #region Add Admin Role
        public async Task<Response<string>> Handle(AddAdminRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserToBeAdminId.ToString());
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
            var user = await _userManager.FindByIdAsync(request.UserToRemoveAdminId.ToString());

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
