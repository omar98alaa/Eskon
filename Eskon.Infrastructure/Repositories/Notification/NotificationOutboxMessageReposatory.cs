using Eskon.Domian.Entities;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;

namespace Eskon.Infrastructure.Repositories
{
    public class NotificationOutboxMessageReposatory : GenericRepositoryAsync<NotificationOutboxMessage>, INotificationOutboxMessageReposatory
    {
        public NotificationOutboxMessageReposatory(MyDbContext myDbContext) : base(myDbContext)
        {
        }
      
    }
} 