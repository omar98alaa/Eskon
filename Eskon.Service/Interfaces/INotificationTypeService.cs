
using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface INotificationTypeService
    {
        #region Create
        public Task<NotificationType> AddNotificationTypeAsync(NotificationType notificationType);
        #endregion

        #region Read
        public Task<NotificationType> GetNotificationTypeByIdAsync(Guid id); 
        public Task<NotificationType> GetNotificationTypeByNameAsync(string name);
        public Task<List<NotificationType>> GetAllNotificationTypesAsync();
        #endregion

        #region Update
        public Task EditNotificationTypeAsync(NotificationType notificationType);
        #endregion

        #region Delete
        public Task SoftDeleteNotificationTypeAsync(NotificationType notificationType);
        #endregion

    }
}
