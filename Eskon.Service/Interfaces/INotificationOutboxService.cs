using Eskon.Domian.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eskon.Service.Interfaces
{
    public interface INotificationOutboxService
    {
        Task<NotificationOutboxMessage> AddAsync(NotificationOutboxMessage message);
        Task<List<NotificationOutboxMessage>> GetPendingAsync();
        Task UpdateStatusAsync(Guid id, string status, string? error = null);
    }
} 