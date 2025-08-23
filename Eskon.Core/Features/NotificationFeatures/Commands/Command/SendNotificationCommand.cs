using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.DTOs.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.NotificationFeatures.Commands.Command
{
    public record SendNotificationCommand(Guid ReceiverId, string Content, string NotificationTypeName,Guid RedirectionId, string RedirectionName) : IRequest<NotificationDto?>;

}
