using AutoMapper;
using Eskon.Core.Features.ChatFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using Eskon.Infrastructure.Interfaces;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Queries.Handler
{
    public class GetUserConversationQueryHandler : ResponseHandler, IRequestHandler<GetUserConversationsQuery, List<ConversationDto>>
    {
        private readonly IChatMessageRepository _chatmessageRepository;
        private readonly IMapper _mapper;

        public GetUserConversationQueryHandler(IChatMessageRepository chatmessageRepository, IMapper mapper)
        {
            _chatmessageRepository = chatmessageRepository;
            _mapper = mapper;
        }

        public async Task<List<ConversationDto>> Handle(GetUserConversationsQuery request, CancellationToken cancellationToken)
        {
            return await _chatmessageRepository.GetConversationsForUserAsync(request.UserId);
        }
    }
}
