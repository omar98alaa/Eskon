using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IUserRepository : IGenericRepositoryAsync<User>
    {
        public Task<User> GetUserByEmailAsync(string email);
    }
}
