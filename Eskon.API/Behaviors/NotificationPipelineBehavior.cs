//using Eskon.API.Hubs;
//using Eskon.Core.Features.NotificationFeatures.Commands.Command;
//using Eskon.Domian.DTOs.Notification;
//using MediatR;
//using Microsoft.AspNetCore.SignalR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Eskon.Core.Behaviors
//{
//    public class NotificationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//    {
//        private readonly IHubContext<NotificationHub> _hubContext;

//        public NotificationPipelineBehavior(IHubContext<NotificationHub> hubContext)
//        {
//            _hubContext = hubContext;
//        }

//        public async Task<TResponse> Handle(
//     TRequest request,
//     RequestHandlerDelegate<TResponse> next,
//     CancellationToken cancellationToken)
//        {
//        //    // 1. نفذ الهاندلر الأول
//        //    var response = await next();

//        //    // 2. لو هو SendNotificationCommand
//        //    if (request is SendNotificationCommand sendNotificationCmd && response is NotificationDto notification)
//        //    {
//        //        // اتسجل في DB (Id != default)
//        //        if (notification.Id != Guid.Empty)
//        //        {
//        //            // ابعته لليوزر عن طريق SignalR
//        //            await _hubContext.Clients
//        //                .User(Guid.Parse(notification.ReciverId)) // ReceiverId ييجي من الـ Handler
//        //                .SendAsync("ReceiveNotification", notification, cancellationToken);
//        //        }
//        //    }

//        //    return response;
//        //}

//    }

//}
