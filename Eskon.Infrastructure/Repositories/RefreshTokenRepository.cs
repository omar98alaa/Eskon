using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken> , IRefreshTokenRepository
    {
        #region Fields
        private readonly DbSet<UserRefreshToken> _refreshTokensDbSet;
        #endregion

        #region Constructors
        public RefreshTokenRepository(MyDbContext myDbContext):base(myDbContext)
        {
            _refreshTokensDbSet = myDbContext.Set<UserRefreshToken>();
        }
        #endregion

        #region Actions
        public async Task AddRefreshTokenAsync(UserRefreshToken userRefreshToken)
        {
            _refreshTokensDbSet.Add(userRefreshToken);
        }

        public async Task RemoveNonRevokedRefreshTokensByUserId(Guid userId)
        {
            var oldTokens = _refreshTokensDbSet.Where(t => t.UserId == userId && !t.IsRevoked);
            _refreshTokensDbSet.RemoveRange(oldTokens);
        }

        public async Task<UserRefreshToken?> GetStoredTokenAsync(string refreshToken)
        {
            return await _refreshTokensDbSet
             .Include(t => t.User)
             .FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
        }

        public async Task<UserRefreshToken?> GetTokenByUserIdAsync(Guid userId)
        {
            return await _refreshTokensDbSet
                .Where(rt => rt.UserId == userId && !rt.IsRevoked).SingleOrDefaultAsync();
        }

        #endregion

    }
}
