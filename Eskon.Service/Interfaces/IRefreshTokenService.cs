using Eskon.Domian.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Eskon.Service.Interfaces
{
    public interface IRefreshTokenService
    {
        public Task SaveRefreshTokenAsync(string token, IdentityUser<Guid> user);
        public Task<UserRefreshToken?> GetStoredTokenAsync(string refreshToken);
        public Task<UserRefreshToken?> GetTokenByUserIdAsync(Guid userId);
        public Task<int> SaveChangesAsync();
    }
}
