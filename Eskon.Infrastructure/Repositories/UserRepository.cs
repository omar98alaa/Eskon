using Microsoft.EntityFrameworkCore;
using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;

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
            return  _userDbSet.FirstOrDefault(s => s.Email == email);
        }
        #endregion
    }
}
