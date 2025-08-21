using Eskon.Core.Features.ChatFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Queries.Handler
{
    interface IChatQueryHandler : IRequestHandler<GetMessagesPerChatQuery, Response<List<ChatMessageDto>>>,
                                  IRequestHandler<GetUserConversationsQuery, Response<List<ConversationDto>>>,
                                  IRequestHandler<GetLastReceivedMessageQuery, Response<ChatMessageDto>>;
}
