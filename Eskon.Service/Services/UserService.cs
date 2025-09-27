using System.ComponentModel.DataAnnotations;
using Eskon.Domain.Utilities;
using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class UserService : IUserService
    {

        #region Fields
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructors
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        public async Task<User?> AddUserAsync(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByStripeAccountIdAsync(string stripeAccountId)
        {
            return await _userRepository.GetUserByStripeAccountIdAsync(stripeAccountId);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<Paginated<User>> GetUsersByRolePaginated(int pageNumber, int itemsPerPage, string role)
        {
            return await _userRepository.GetPaginatedAsync(pageNumber, itemsPerPage, filter: u => u.UserRoles.Any(r => r.Role.NormalizedName == role.ToUpperInvariant()));
        }

        public async Task<bool> SetUserStripeAccountIdAsync(Guid userId, string stripeAccountId)
        {
            return await _userRepository.SetUserStripeAccountIdAsync(userId, stripeAccountId);
        }

        public Task<int> CountUsersByRoleAsync(string role)
        {
            return _userRepository.CountUsersByRoleAsync(role);
        }
    }
}
