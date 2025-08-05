using Eskon.Core.Features.ChatFeatures.Commands.Command;
using Eskon.Domian.DTOs.Chat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Eskon.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IMediator mediator, ILogger<ChatHub> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(SendMessageDto dto)
        {

                var command = new SendMessageCommand(dto);
                var messageDto = await _mediator.Send(command);

                var receiverGroup = messageDto.ReceiverId.ToString();
                var senderGroup = messageDto.SenderId.ToString();

                await Clients.User(receiverGroup).SendAsync("ReceiveMessage", messageDto);
                await Clients.User(senderGroup).SendAsync("ReceiveMessage", messageDto);
            
  
        }

    }
}
