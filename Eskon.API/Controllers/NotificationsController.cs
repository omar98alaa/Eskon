using Eskon.API.Base;
using Eskon.Core.Features.NotificationFeatures.Commands.Command;
using Eskon.Core.Features.NotificationFeatures.Queries.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : BaseController
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserNotifications()
        {
            Guid userId = GetUserIdFromAuthenticatedUserToken();

            var response = await _mediator.Send(new GetNotificationsPerUserQuery(userId));
            return Ok(response);
        }

        [HttpPut("markAsRead/{notificationId:guid}")]
        public async Task<IActionResult> MarkAsRead([FromRoute] Guid notificationId)
        {
            Guid userId = GetUserIdFromAuthenticatedUserToken();

            var response = await _mediator.Send(new MarkNotificationAsReadCommand(userId, notificationId));
            return Ok(response);
        }

    }
}
