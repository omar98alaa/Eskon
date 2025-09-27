using Eskon.Domian.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Eskon.Service.Interfaces
{
    public interface IRefreshTokenService
    {
        public Task AddNewRefreshTokenForUserAsync(UserRefreshToken userRefreshToken);
        public Task RemoveNonRevokedRefreshTokensForUser(User user);
        public Task<UserRefreshToken?> GetStoredTokenAsync(string refreshToken);
        public Task<UserRefreshToken?> GetTokenByUserIdAsync(Guid userId);
    }
}
