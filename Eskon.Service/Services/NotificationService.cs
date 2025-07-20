using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class NotificationService : INotificationService
    {

        #region Fields
        private readonly INotificationRepository _notificationRepository;
        #endregion

        #region Constructors
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        #endregion

        #region Methods
        #region Create
        public async Task<Notification> AddNewNotificationAsync(Notification notification)
        {
            return await _notificationRepository.AddAsync(notification);
        }
        #endregion

        #region Read
        public async Task<List<Notification>> GetAllNotificationsForSpecificNotificationTypeIdAsync(Guid notificationTypeId)
        {
            return await _notificationRepository.GetAllNotificationsForSpecificNotificationTypeIdAsync(notificationTypeId);
        }
        public async Task<List<Notification>> GetAllNotificationsForSpecificRecieverIdAsync(Guid recieverId)
        {
            return await _notificationRepository.GetAllNotificationsForSpecificRecieverIdAsync(recieverId);
        }
        public async Task<List<Notification>> GetAllNotificationsForSpecificRedirectionIdAsync(Guid redirectionId)
        {
            return await _notificationRepository.GetAllNotificationsForSpecificRedirectionIdAsync(redirectionId);
        }
        #endregion

        #region Update
        public async Task UpdateNotification(Notification notification)
        {
            notification.UpdatedAt = DateTime.UtcNow;
            await _notificationRepository.UpdateAsync(notification);
        }
        #endregion

        #region Delete
        public async Task DeleteNotification(Notification notification)
        {
            await _notificationRepository.DeleteAsync(notification);
        }

        public async Task SoftDeleteNotification(Notification notification)
        {
            await _notificationRepository.SoftDeleteAsync(notification);
        }
        #endregion

        public async Task<int> SaveChangesAsync()
        {
            return await _notificationRepository.SaveChangesAsync();
        } 
        #endregion

    }
}
