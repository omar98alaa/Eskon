using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Service.Interfaces;
using MediatR;

namespace Eskon.Core.Features.UserRolesFeatures.Utilities
{
    public class GetRefreshTokenHandler :ResponseHandler, IRequestHandler<GetRefreshToken, Response<TokenResponseDto>>
    {
        #region Fields
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructors
        public GetRefreshTokenHandler(IRefreshTokenService refreshTokenService, IAuthenticationService authenticationService)
        {
            _refreshTokenService = refreshTokenService;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Actions
        public async Task<Response<TokenResponseDto>> Handle(GetRefreshToken request, CancellationToken cancellationToken)
        {
            var storedToken = await _refreshTokenService.GetStoredTokenAsync(request.token);

            if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
            {
                return Unauthorized<TokenResponseDto>();
            }

            var user = storedToken.User;

            // Do not generate a new refresh token
            var newAccessToken = await _authenticationService.GenerateJWTTokenAsync(user);

            return Success(new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = storedToken.RefreshToken
            });
        }
        #endregion
    }
}
