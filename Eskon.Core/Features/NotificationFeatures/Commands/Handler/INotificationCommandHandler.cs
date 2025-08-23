using Eskon.Core.Features.ChatFeatures.Commands.Command;
using Eskon.Core.Features.NotificationFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.DTOs.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.NotificationFeatures.Commands.Handler
{
    interface INotificationCommandHandler  : IRequestHandler<SendNotificationCommand, NotificationDto?>,
                                             IRequestHandler<MarkNotificationAsReadCommand, Response<Unit>>
    {
    }
}
