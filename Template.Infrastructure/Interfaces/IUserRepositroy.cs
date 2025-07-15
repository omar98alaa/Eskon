using Template.Domian.Entities.Identity;
using Template.Infrastructure.Generics;

namespace Template.Infrastructure.Interfaces
{
    public interface IUserRepository : IGenericRepositoryAsync<User>
    {
        public Task<User> GetUserByEmailAsync(string email);
    }
}
