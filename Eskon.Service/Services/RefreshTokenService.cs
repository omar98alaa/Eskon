using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Eskon.Service.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        #region Fields
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        #endregion

        #region Constructors
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        #endregion

        #region Actions
        public async Task AddNewRefreshTokenForUserAsync(UserRefreshToken userRefreshToken)
        {
            await _refreshTokenRepository.AddRefreshTokenAsync(userRefreshToken);
        }

        public async Task RemoveNonRevokedRefreshTokensForUser(User user)
        {
            await _refreshTokenRepository.RemoveNonRevokedRefreshTokensByUserId(user.Id);
        }

        public async Task<UserRefreshToken?> GetStoredTokenAsync(string refreshToken)
        {
            return await _refreshTokenRepository.GetStoredTokenAsync(refreshToken);
        }
        public async Task<UserRefreshToken?> GetTokenByUserIdAsync(Guid userId)
        {
            return await _refreshTokenRepository.GetTokenByUserIdAsync(userId);
        }

        #endregion
    }
}
