using AutoMapper;
using Eskon.Core.Features.ChatFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Commands.Handler
{
    public class ChatCommandHandler : ResponseHandler, IChatCommandHandler
    {
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;

        public ChatCommandHandler(IServiceUnitOfWork unitOfWork, IMapper mapper)
        {
            _serviceUnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ChatMessageDto?> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await _serviceUnitOfWork.ChatService.GetChatByIdAsync(request.MessageDto.ChatId);
            if (chat == null)
            {
                return null;
            }

            var newChatMessage = new ChatMessage()
            {
                ChatId = chat.Id,
                SenderId = request.senderId,
                Content = request.MessageDto.Content,
            };

            chat.ChatMessages.Add(newChatMessage);

            await _serviceUnitOfWork.SaveChangesAsync();
            return _mapper.Map<ChatMessageDto>(newChatMessage);
        }

        public async Task<Response<Unit>> Handle(MarkMessagesAsReadCommand request, CancellationToken cancellationToken)
        {
            var chat = await _serviceUnitOfWork.ChatService.GetChatByIdAsync(request.ChatId);
            if (chat == null)
            {
                return NotFound<Unit>("Chat does not exist");
            }

            if (chat.User1Id != request.UserId && chat.User2Id != request.UserId)
            {
                return Forbidden<Unit>();
            }

            await _serviceUnitOfWork.ChatService.MarkMessagesAsRead(chat);

            return Success(Unit.Value);
        }

        public async Task<Response<ConversationDto>> Handle(AddNewChatCommand request, CancellationToken cancellationToken)
        {
            var user2 = await _serviceUnitOfWork.UserService.GetUserByIdAsync(request.user2Id);
            if(user2 == null)
            {
                return NotFound<ConversationDto>("Other user does not exist");
            }

            var chat = await _serviceUnitOfWork.ChatService.GetChatBetweenUsersAsync(new User() { Id = request.user1Id }, user2);
            
            if (chat == null)
            {
                chat = new Chat() { User1Id = request.user1Id, User2Id = request.user2Id };

                await _serviceUnitOfWork.ChatService.AddChatAsync(chat);
                await _serviceUnitOfWork.SaveChangesAsync();
            }

            var chatDTO = _mapper.Map<ConversationDto>(chat);

            return Created(chatDTO, "Chat created successfully");
        }
    }
}