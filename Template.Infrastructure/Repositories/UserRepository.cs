using Microsoft.EntityFrameworkCore;
using Template.Domian.Entities.Identity;
using Template.Infrastructure.Context;
using Template.Infrastructure.Generics;
using Template.Infrastructure.Interfaces;

namespace Template.Infrastructure.Repositories
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
