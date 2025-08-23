
using Eskon.Domian.Entities;
using Eskon.Domian.Entities;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface INotificationService
    {
        #region Create
        public Task<Notification> AddNewNotificationAsync(Notification notification);
        #endregion

        #region Read
        public Task<List<Notification>> GetAllNotificationsForSpecificNotificationTypeIdAsync(Guid notificationTypeId);
        public Task<List<Notification>> GetAllNotificationsForSpecificRecieverAsync(User recieverUser);
        public Task<List<Notification>> GetAllNotificationsForSpecificRedirectionIdAsync(Guid redirectionId);
        #endregion

        #region Update
        public Task UpdateNotification(Notification notification);
        public void SetNotificationAsReadAsync(Notification notification);
        #endregion

        #region Delete
        public Task SoftDeleteNotification(Notification notification);
        #endregion

    }
}
