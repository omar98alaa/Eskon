
using Eskon.API.Hubs;
using Eskon.Core.Features.NotificationFeatures.Events.Event;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.NotificationFeatures.Events.Handler
{
    public class NotificationEventHandler :
        INotificationHandler<NotificationCreatedEvent>
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationEventHandler(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Handle(NotificationCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.User(notification.ReceiverId.ToString())
                .SendAsync("ReceiveNotification", new
                {
                    id = notification.NotificationId,
                    content = notification.Content,
                    isRead = false,
                    createdAt = notification.CreatedAt,
                    notificationTypeName = notification.NotificationTypeName
                }, cancellationToken);
        }
    }
}

