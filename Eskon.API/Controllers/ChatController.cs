using Eskon.Core.Features.ChatFeatures.Commands.Command;
using Eskon.Core.Features.ChatFeatures.Queries.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Chat/messages/{chatId}
        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages(Guid chatId)
        {
            var result = await _mediator.Send(new GetMessagesPerChatQuery(chatId));
            return Ok(result);
        }

        // GET: api/Chat/conversations/{userId}
        [HttpGet("conversations/{userId}")]
        public async Task<IActionResult> GetUserConversations(Guid userId)
        {
            var result = await _mediator.Send(new GetUserConversationsQuery(userId));
            return Ok(result);
        }
        [HttpPost("mark-as-read/{chatId}")]
        public async Task<IActionResult> MarkMessagesAsRead(Guid chatId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value))
                return Unauthorized();

            Guid userId = Guid.Parse(userIdClaim.Value);
            await _mediator.Send(new MarkMessagesAsReadCommand(chatId, userId));
            return Ok();
        }


    }
}
