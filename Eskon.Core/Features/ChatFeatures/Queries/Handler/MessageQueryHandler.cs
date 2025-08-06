using AutoMapper;
using Eskon.Core.Features.ChatFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.UnitOfWork;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Queries.Handler
{
    public class MessageQueryHandler : ResponseHandler, IRequestHandler<GetMessagesPerChatQuery, List<ChatMessageDto>>,
        IRequestHandler<GetUserConversationsQuery, List<ConversationDto>>
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;

        public MessageQueryHandler(IServiceUnitOfWork serviceUnitOfWork, IMapper mapper)
        {
           _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ChatMessageDto>> Handle(GetMessagesPerChatQuery request, CancellationToken cancellationToken)
        {
            var messages = await _serviceUnitOfWork.ChatMessagesService.GetMessagesByChatIdAsync(request.ChatId);
            return _mapper.Map<List<ChatMessageDto>>(messages);
        }
        public async Task<List<ConversationDto>> Handle(GetUserConversationsQuery request, CancellationToken cancellationToken)
        {
            return await _serviceUnitOfWork.ChatMessagesService.GetConversationsForUserAsync(request.UserId);
        }
    }

}
