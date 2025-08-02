using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IUserRepository : IGenericRepositoryAsync<User>
    {
        public Task<User> GetUserByEmailAsync(string email);
        public Task<User> GetUserByStripeAccountIdAsync(string stripeAccountId);
        public Task<bool> SetUserStripeAccountIdAsync(Guid userId, string stripeAccountId);
    }
}
