using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface INotificationRepository : IGenericRepositoryAsync<Notification>
    {
        public Task<List<Notification>> GetAllNotificationsForSpecificRedirectionIdAsync(Guid redirectionId);
        public Task<List<Notification>> GetAllNotificationsForSpecificRecieverIdAsync(Guid recieverId);
        public Task<List<Notification>> GetAllNotificationsForSpecificNotificationTypeIdAsync(Guid notificationTypeId);
    }
}
