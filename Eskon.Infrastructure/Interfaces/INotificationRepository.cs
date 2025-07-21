using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface INotificationRepository : IGenericRepositoryAsync<Notification>
    {
        #region Read
        public Task<List<Notification>> GetAllNotificationsForSpecificRedirectionIdAsync(Guid redirectionId);
        public Task<List<Notification>> GetAllNotificationsForSpecificRecieverAsync(User recieverUser);
        public Task<List<Notification>> GetAllNotificationsForSpecificNotificationTypeIdAsync(Guid notificationTypeId);
        #endregion
        #region Update
        public void SetNotificationAsReadAsync(Notification notification); 
        #endregion
    }
}
