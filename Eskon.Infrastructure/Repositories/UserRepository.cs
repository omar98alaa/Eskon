using Microsoft.EntityFrameworkCore;
using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Eskon.Domian.Entities;

namespace Eskon.Infrastructure.Repositories
{
    public class UserRepository : GenericRepositoryAsync<User>, IUserRepository
    {
        #region Fields
        private readonly DbSet<User> _userDbSet;
        #endregion

        #region Constructors
        public UserRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _userDbSet = myDbContext.Set<User>();
        }
        #endregion

        #region Methods
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return  await _userDbSet.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<bool> SetUserStripeAccountIdAsync(Guid userId, string stripeAccountId)
        {
            var user = await _userDbSet.FirstOrDefaultAsync(u => u.Id ==  userId);
            if (user == null)
            {
                return false;
            }
            user.stripeAccountId = stripeAccountId;
            _userDbSet.Update(user);
            return true;
        }

        public async Task<User> GetUserByStripeAccountIdAsync(string stripeAccountId)
        {
            return await _userDbSet.FirstOrDefaultAsync(s => s.stripeAccountId == stripeAccountId);
        }
        #endregion
    }
}
