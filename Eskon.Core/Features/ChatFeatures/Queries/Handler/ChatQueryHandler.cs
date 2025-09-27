using AutoMapper;
using Eskon.Core.Features.ChatFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.UnitOfWork;

namespace Eskon.Core.Features.ChatFeatures.Queries.Handler
{
    public class ChatQueryHandler : ResponseHandler, IChatQueryHandler
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;

        public ChatQueryHandler(IServiceUnitOfWork serviceUnitOfWork, IMapper mapper)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<List<ChatMessageDto>>> Handle(GetMessagesPerChatQuery request, CancellationToken cancellationToken)
        {
            var chat = await _serviceUnitOfWork.ChatService.GetChatByIdAsync(request.ChatId);
            if (chat == null)
            {
                return NotFound<List<ChatMessageDto>>("Chat does not exist");
            }

            if (chat.User1Id != request.userId && chat.User2Id != request.userId)
            {
                return Forbidden<List<ChatMessageDto>>();
            }

            var messages = chat.ChatMessages;
            var messagesDTO = _mapper.Map<List<ChatMessageDto>>(messages);

            return Success(messagesDTO);
        }

        public async Task<Response<List<ConversationDto>>> Handle(GetUserConversationsQuery request, CancellationToken cancellationToken)
        {
            var chats = await _serviceUnitOfWork.ChatService.GetAllUserChatsAsync(new User() { Id = request.UserId });

            var chatsDTO = _mapper.Map<List<ConversationDto>>(chats, opt =>
            {
                opt.Items["CurrentUserId"] = request.UserId;
            });

            return Success(chatsDTO);
        }

        public async Task<Response<ChatMessageDto>> Handle(GetLastReceivedMessageQuery request, CancellationToken cancellationToken)
        {
            var lastMessage = await _serviceUnitOfWork.ChatMessagesService
                .GetlastMessagesAsync(request.chat, request.UserId);

            if (lastMessage == null)
                return null;

            var lastMessageDto = _mapper.Map<ChatMessageDto>(lastMessage);

            return Success(lastMessageDto);
        }


    }

}
