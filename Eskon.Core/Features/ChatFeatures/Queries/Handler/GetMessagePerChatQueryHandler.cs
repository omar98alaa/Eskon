using AutoMapper;
using Eskon.Core.Features.ChatFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using Eskon.Infrastructure.Interfaces;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Queries.Handler
{
    public class GetMessagesPerChatQueryHandler : ResponseHandler, IRequestHandler<GetMessagesPerChatQuery, List<ChatMessageDto>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IMapper _mapper;

        public GetMessagesPerChatQueryHandler(IChatMessageRepository chatMessageRepository, IMapper mapper)
        {
            _chatMessageRepository = chatMessageRepository;
            _mapper = mapper;
        }

        public async Task<List<ChatMessageDto>> Handle(GetMessagesPerChatQuery request, CancellationToken cancellationToken)
        {
            var messages = await _chatMessageRepository.GetMessagesByChatIdAsync(request.ChatId);
            return _mapper.Map<List<ChatMessageDto>>(messages);
        }
    }

}
