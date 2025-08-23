using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.NotificationFeatures.Queries.Query
{
    public record GetNotificationsPerUserQuery(Guid userId) : IRequest<Response<List<NotificationDto>>>;
}
