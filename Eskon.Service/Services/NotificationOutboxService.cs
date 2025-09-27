using Eskon.Domian.Entities;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Service.Services
{
    public class NotificationOutboxService : INotificationOutboxService
    {
        private readonly INotificationOutboxMessageReposatory _notificationOutboxRepository;

        public NotificationOutboxService(INotificationOutboxMessageReposatory notificationOutboxRepository)
        {
            _notificationOutboxRepository = notificationOutboxRepository;
        }

        public async Task<NotificationOutboxMessage> AddAsync(NotificationOutboxMessage message)
        {
            await _notificationOutboxRepository.AddAsync(message);
            return message;
        }

        public async Task<List<NotificationOutboxMessage>> GetPendingAsync()
        {
            return await _notificationOutboxRepository
                .GetFilteredAsync(m => m.Status == "Pending");
        }

        public async Task UpdateStatusAsync(Guid id, string status, string? error = null)
        {
            var msg = await _notificationOutboxRepository.GetByIdAsync(id);
            if (msg != null)
            {
                msg.Status = status;
                msg.Error = error;
                msg.LastTriedAt = DateTime.UtcNow;
                await _notificationOutboxRepository.UpdateAsync(msg);
            }
        }
    }
} 