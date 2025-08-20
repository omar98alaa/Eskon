using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Entities;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Eskon.Domian.Models;

namespace Eskon.Infrastructure.Repositories
{
    public class NotificationRepositroy : GenericRepositoryAsync<Notification>, INotificationRepository
    {
        #region
        private readonly DbSet<Notification> _notificationRepository;
        #endregion

        #region Constructors
        public NotificationRepositroy(MyDbContext myDbContext) : base(myDbContext)
        {
            _notificationRepository = myDbContext.Set<Notification>();
        }
        #endregion

        #region Methods
        #region Read
        public async Task<List<Notification>> GetAllNotificationsForSpecificNotificationTypeIdAsync(Guid notificationTypeId)
        {
            return await _notificationRepository.Where(n => n.NotificationTypeId == notificationTypeId).ToListAsync();
        }

        public async Task<List<Notification>> GetAllNotificationsForSpecificRecieverAsync(User recieverUser)
        {
            return await _notificationRepository.Where(n => n.ReceiverId == recieverUser.Id).ToListAsync();
        }

        public async Task<List<Notification>> GetAllNotificationsForSpecificRedirectionIdAsync(Guid redirectionId)
        {
            return await _notificationRepository.Where(n => n.RedirectionId == redirectionId).ToListAsync();
        }

        #endregion
        #region Update
        public void SetNotificationAsReadAsync(Notification notification)
        {
            notification.IsRead = true;
        }
        #endregion
        #endregion
    }
}
