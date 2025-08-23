using Eskon.Core.Features.ChatFeatures.Queries.Query;
using Eskon.Core.Features.NotificationFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.DTOs.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.NotificationFeatures.Queries.Handler
{
    interface INotificationQueryHandler : IRequestHandler<GetNotificationsPerUserQuery, Response<List<NotificationDto>>>;
}
