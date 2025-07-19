using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Eskon.Core.Features.UserRolesFeatures.Utilities
{
    public class GetNewAccessTokenHandler :ResponseHandler, IRequestHandler<GetNewAccessToken, Response<TokenResponseDto>>
    {
        #region Fields
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public GetNewAccessTokenHandler(IRefreshTokenService refreshTokenService, IAuthenticationService authenticationService, UserManager<User> userManager)
        {
            _refreshTokenService = refreshTokenService;
            _authenticationService = authenticationService;
            _userManager = userManager;
        }
        #endregion

        #region Actions
        public async Task<Response<TokenResponseDto>> Handle(GetNewAccessToken request, CancellationToken cancellationToken)
        {
            var storedToken = await _refreshTokenService.GetStoredTokenAsync(request.RefreshToken);

            if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
            {
                return Unauthorized<TokenResponseDto>();
            }

            var user = storedToken.User;

            var userManagerClaims = await _userManager.GetClaimsAsync(user);
            var userManagerRoles = await _userManager.GetRolesAsync(user);

            var newAccessToken = _authenticationService.GenerateJWTTokenAsync(user, userManagerRoles, userManagerClaims);

            return Success(new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = storedToken.RefreshToken
            });
        }
        #endregion
    }
}
