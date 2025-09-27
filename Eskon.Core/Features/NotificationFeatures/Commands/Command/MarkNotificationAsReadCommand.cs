using Eskon.Core.Response;
using Eskon.Domian.DTOs.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.NotificationFeatures.Commands.Command
{
    public record MarkNotificationAsReadCommand(Guid UserId, Guid NotificationId) : IRequest<Response<Unit>>;
}
