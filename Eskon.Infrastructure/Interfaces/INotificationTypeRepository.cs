
using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface INotificationTypeRepository : IGenericRepositoryAsync<NotificationType>
    {
        public Task<NotificationType> GetNotificationTypeByNameAsync(string name);
    }
}
