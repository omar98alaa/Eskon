using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Generics;
using Microsoft.AspNetCore.Identity;


namespace Eskon.Infrastructure.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
        public Task SaveRefreshTokenAsync(string token, IdentityUser<Guid> user);
        public Task<UserRefreshToken?> GetStoredTokenAsync(string refreshToken);
        public Task<UserRefreshToken?> GetTokenByUserIdAsync(Guid userId);
    }
}
