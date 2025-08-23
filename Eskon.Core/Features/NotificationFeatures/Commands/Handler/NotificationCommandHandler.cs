using AutoMapper;
using Eskon.Core.Features.NotificationFeatures.Commands.Command;
using Eskon.Domian.DTOs.Notification;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;
using MediatR;
using Eskon.Core.Response;
using Eskon.Domian.Entities;

namespace Eskon.Core.Features.NotificationFeatures.Commands.Handler
{
    class NotificationCommandHandler : ResponseHandler, INotificationCommandHandler
    {

        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMediator _mediator;

        public NotificationCommandHandler(IServiceUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _serviceUnitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<NotificationDto?> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {

            var type = await _serviceUnitOfWork.NotificationTypeService.GetNotificationTypeByNameAsync(request.NotificationTypeName);
            Console.WriteLine("[send command n type]" + type);
            if (type == null)
                throw new Exception($"NotificationType '{request.NotificationTypeName}' not found.");

            var notification = new Notification
            {
                Content = request.Content,
                ReceiverId = request.ReceiverId,
                RedirectionId =  Guid.Empty,
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                NotificationTypeId = type.Id
            };

            await _serviceUnitOfWork.NotificationService.AddNewNotificationAsync(notification);
            await _serviceUnitOfWork.SaveChangesAsync();


            var outboxMessage = new NotificationOutboxMessage
            {
                Payload = System.Text.Json.JsonSerializer.Serialize(new {
                    NotificationId = notification.Id,
                    ReceiverId = notification.ReceiverId,
                    Content = notification.Content,
                    NotificationTypeName = type.Name,
                    CreatedAt = notification.CreatedAt
                }),
                Type = "Notification",
                Status = "Pending"
            };
            await _serviceUnitOfWork.NotificationOutboxService.AddAsync(outboxMessage);
            await _serviceUnitOfWork.SaveChangesAsync();

            // await _mediator.Publish(...)

            return new NotificationDto
            {
                Id = notification.Id,
                ReceiverId = notification.ReceiverId,
                Content = notification.Content,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt,
                NotificationTypeName = type.Name,
                RedirectionId = notification.RedirectionId,
                RedirectionName = null
            };
        }


        public async Task<Response<Unit>> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
           
            var allNotifications = await _serviceUnitOfWork.NotificationService
                 .GetAllNotificationsForSpecificRecieverAsync(new User { Id = request.UserId });

            var notification = allNotifications.FirstOrDefault(n => n.Id == request.NotificationId);
            if (notification == null)
            {
                return NotFound<Unit>("Notification does not exist");
            }

            if (notification.ReceiverId != request.UserId)
            {
                return Forbidden<Unit>();
            }

            notification.IsRead = true;
            await _serviceUnitOfWork.NotificationService.UpdateNotification(notification);
            await _serviceUnitOfWork.SaveChangesAsync();

            return Success(Unit.Value);
        }

    }
}
