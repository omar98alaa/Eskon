using AutoMapper;
using Eskon.Core.Features.NotificationFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.DTOs.Notification;
using Eskon.Domian.DTOs.Property;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.NotificationFeatures.Queries.Handler
{
    class NotificationQueryHandler : ResponseHandler, INotificationQueryHandler
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;

        public NotificationQueryHandler(IServiceUnitOfWork serviceUnitOfWork, IMapper mapper)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<NotificationDto>>> Handle(GetNotificationsPerUserQuery request, CancellationToken cancellationToken)
        {
            var userNotifications = await _serviceUnitOfWork.NotificationService.GetAllNotificationsForSpecificRecieverAsync(new User() { Id = request.userId });

            var userNotificationsDTO = _mapper.Map<List<NotificationDto>>(userNotifications);

            return Success(userNotificationsDTO); 
        }
    }
}
