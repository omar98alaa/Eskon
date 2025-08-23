using Eskon.Domian.Entities;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class NotificationTypeService : INotificationTypeService
    {
        #region Fields
        private readonly INotificationTypeRepository _notificationTypeRepository;
        #endregion

        #region Constructors
        public NotificationTypeService(INotificationTypeRepository notificationTypeRepository)
        {
            _notificationTypeRepository = notificationTypeRepository;
        }
        #endregion

        #region Methods

        #region Create
        public async Task<NotificationType> AddNotificationTypeAsync(NotificationType notificationType)
        {
            return await _notificationTypeRepository.AddAsync(notificationType);
        }
        #endregion

        #region Read
        public async Task<NotificationType> GetNotificationTypeByIdAsync(Guid id)
        {
            return await _notificationTypeRepository.GetByIdAsync(id);
        }

        public async Task<NotificationType> GetNotificationTypeByNameAsync(string name)
        {
            return await _notificationTypeRepository.GetNotificationTypeByNameAsync(name);
        }

        public async Task<List<NotificationType>> GetAllNotificationTypesAsync()
        {
            return await _notificationTypeRepository.GetAllAsync();
        }
        #endregion

        #region Update
        public async Task EditNotificationTypeAsync(NotificationType notificationType)
        {
            notificationType.UpdatedAt = DateTime.UtcNow;
            await _notificationTypeRepository.UpdateAsync(notificationType);
        }
        #endregion

        #region Delete
        public async Task SoftDeleteNotificationTypeAsync(NotificationType notificationType)
        {
            await _notificationTypeRepository.SoftDeleteAsync(notificationType);
        }
        #endregion

        #endregion
    }
}
