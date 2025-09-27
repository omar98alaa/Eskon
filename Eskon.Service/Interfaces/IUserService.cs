using Eskon.Domain.Utilities;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Service.Interfaces
{
    public interface IUserService
    {
        public Task<User?> AddUserAsync(User user);

        #region Read
        public Task<User> GetUserByIdAsync(Guid id);

        public Task<User> GetUserByStripeAccountIdAsync(string stripeAccountId);
        public Task<List<User>> GetAllUsersAsync();
        public Task<User> GetUserByEmailAsync(string email);
        public Task<Paginated<User>> GetUsersByRolePaginated(int pageNumber, int itemsPerPage, string role); 
        #endregion
        public Task<bool> SetUserStripeAccountIdAsync(Guid userId, string stripeAccountId);

        Task<int> CountUsersByRoleAsync(string role);


    }
}
