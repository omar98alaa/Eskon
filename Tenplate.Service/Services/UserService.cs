using System.ComponentModel.DataAnnotations;
using Template.Domian.Entities.Identity;
using Template.Infrastructure.Interfaces;
using Template.Service.Interfaces;

namespace Template.Service.Services
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

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _userRepository.SaveChangesAsync(); 
        }
    }
}
