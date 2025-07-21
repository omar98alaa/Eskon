using Eskon.Domian.Entities.Identity;
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
        public async Task<List<Notification>> GetAllNotificationsForSpecificRecieverAsync(User recieverUser)
        { 
            return await _notificationRepository.GetAllNotificationsForSpecificRecieverAsync(recieverUser);
        }
        public async Task<List<Notification>> GetAllNotificationsForSpecificRedirectionIdAsync(Guid redirectionId)
        {
            return await _notificationRepository.GetAllNotificationsForSpecificRedirectionIdAsync(redirectionId);
        }
        #endregion

        #region Update
        public async Task UpdateNotification(Notification notification)
        {
            await _notificationRepository.UpdateAsync(notification);
        }
        public void SetNotificationAsReadAsync(Notification notification)
        {
            _notificationRepository.SetNotificationAsReadAsync(notification);
        }
        #endregion

        #region Delete
        public async Task SoftDeleteNotification(Notification notification)
        {
            await _notificationRepository.SoftDeleteAsync(notification);
        }
        #endregion

        #endregion

    }
}
