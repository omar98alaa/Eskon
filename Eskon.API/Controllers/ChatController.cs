using Eskon.API.Base;
using Eskon.Core.Features.ChatFeatures.Commands.Command;
using Eskon.Core.Features.ChatFeatures.Queries.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : BaseController
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Chat/messages/{chatId}
        [HttpGet("messages/{chatId:guid}")]
        public async Task<IActionResult> GetMessages(Guid chatId)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var result = await _mediator.Send(new GetMessagesPerChatQuery(userId, chatId));
            return NewResult(result);
        }

        // GET: api/Chat/conversations/{userId}
        [HttpGet("conversations")]
        public async Task<IActionResult> GetUserConversations()
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var result = await _mediator.Send(new GetUserConversationsQuery(userId));
            return NewResult(result);
        }
        [HttpPost("mark-as-read/{chatId}")]
        public async Task<IActionResult> MarkMessagesAsRead(Guid chatId)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var response = await _mediator.Send(new MarkMessagesAsReadCommand(chatId, userId));
            return NewResult(response);
        }

        [HttpPost("{user2Id:guid}")]
        public async Task<IActionResult> AddNewChat([FromRoute] Guid user2Id)
        {
            var user1Id = GetUserIdFromAuthenticatedUserToken();
            var response = await _mediator.Send(new AddNewChatCommand(user1Id, user2Id));
            return NewResult(response);
        }
    }
}
