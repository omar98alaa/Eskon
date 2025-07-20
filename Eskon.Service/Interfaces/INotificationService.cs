
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
        public Task<List<Notification>> GetAllNotificationsForSpecificRecieverIdAsync(Guid recieverId);
        public Task<List<Notification>> GetAllNotificationsForSpecificRedirectionIdAsync(Guid redirectionId);
        #endregion

        #region Update
        public Task UpdateNotification(Notification notification);
        #endregion

        #region Delete
        public Task DeleteNotification(Notification notification);

        public Task SoftDeleteNotification(Notification notification);
        #endregion

        public Task<int> SaveChangesAsync();
    }
}
