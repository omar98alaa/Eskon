using Eskon.Domain.Utilities;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Service.Interfaces
{
    public interface IUserService
    {
        public Task<User?> AddUserAsync(User user);
        public Task<User> GetUserByIdAsync(Guid id);
        public Task<List<User>> GetAllUsersAsync();
        public Task<User> GetUserByEmailAsync(string email);
        public Task<Paginated<User>> GetUsersByRolePaginated(int pageNumber, int itemsPerPage, string role);

    }
}
