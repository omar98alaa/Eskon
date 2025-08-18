using Eskon.Domian.Entities;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface INotificationOutboxMessageReposatory : IGenericRepositoryAsync<NotificationOutboxMessage>
    {
        
    }
} 