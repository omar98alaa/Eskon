using Template.Domian.Entities.Identity;

namespace Template.Service.Interfaces
{
    public interface IUserService
    {
        public Task<User?> AddUserAsync(User user);
        public Task<User> GetUserByIdAsync(Guid id);
        public Task<List<User>> GetAllUsersAsync();
        public Task<int> SaveChangesAsync();
    }
}
