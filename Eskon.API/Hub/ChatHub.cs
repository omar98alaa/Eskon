using Eskon.Core.Features.ChatFeatures.Commands.Command;
using Eskon.Core.Features.ChatFeatures.Queries.Query;
using Eskon.Domian.DTOs.Chat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

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
            var senderId = GetUserIdFromAuthenticatedUserToken();
            var command = new SendMessageCommand(senderId, dto);
            var messageDto = await _mediator.Send(command);

            var receiverGroup = messageDto.ReceiverId.ToString();
            var senderGroup = messageDto.SenderId.ToString();

            await Clients.User(receiverGroup).SendAsync("ReceiveMessage", messageDto);
            await Clients.User(senderGroup).SendAsync("ReceiveMessage", messageDto);

        
            var unreadCountReceiver = await _mediator.Send(new GetUserConversationsQuery(messageDto.ReceiverId));
            var receiverConv = unreadCountReceiver.Data.FirstOrDefault(c => c.Id == messageDto.ChatId);
            await Clients.User(receiverGroup).SendAsync("UpdateUnreadCount", receiverConv?.UnreadCount ?? 0);

            await Clients.User(senderGroup).SendAsync("MessageDelivered", new
            {
                ChatId = messageDto.ChatId,
                MessageId = messageDto.MessageId,
                ReceiverId = messageDto.ReceiverId,
                DeliveredAt = DateTime.UtcNow
            });
        }

        public async Task MarkChatAsRead(Guid chatId)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var command = new MarkMessagesAsReadCommand(chatId, userId);
            await _mediator.Send(command);

            var unreadCountResponse = await _mediator.Send(new GetUserConversationsQuery(userId));
            var userConv = unreadCountResponse.Data.FirstOrDefault(c => c.Id == chatId);

            await Clients.User(userId.ToString())
                .SendAsync("UpdateUnreadCount", userConv?.UnreadCount ?? 0);

            var lastReceivedMessageResponse = await _mediator.Send(
                new GetLastReceivedMessageQuery(userId, chatId) 
            );

            var lastReceivedMessage = lastReceivedMessageResponse.Data;
            if (lastReceivedMessage != null)
            {
                await Clients.User(lastReceivedMessage.SenderId.ToString())
                    .SendAsync("MessagesSeen", chatId, userId);
            }
        }

        protected Guid GetUserIdFromAuthenticatedUserToken()
        {
            var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier);
            var userId = Guid.Parse(userIdClaim.Value);
            return userId;
        }
    }
}
