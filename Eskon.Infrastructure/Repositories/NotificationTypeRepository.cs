using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class NotificationTypeRepository : GenericRepositoryAsync<NotificationType> , INotificationTypeRepository
    {
        #region Fields
        private readonly DbSet<NotificationType> _notificationTypesDbSet;
        #endregion

        #region Constructors
        public NotificationTypeRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _notificationTypesDbSet = myDbContext.Set<NotificationType>();
        }
        #endregion

        #region Methods

        public async Task<NotificationType> GetNotificationTypeByNameAsync(string name)
        {
            return await _notificationTypesDbSet.FirstOrDefaultAsync(nt => nt.Name == name);
        }
        #endregion
    }
}
