using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Generics;


namespace Eskon.Infrastructure.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
        public Task AddRefreshTokenAsync(UserRefreshToken userRefreshToken);
        public Task RemoveNonRevokedRefreshTokensByUserId(Guid userId);
        public Task<UserRefreshToken?> GetStoredTokenAsync(string refreshToken);
        public Task<UserRefreshToken?> GetTokenByUserIdAsync(Guid userId);
    }
}
