using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Queries.Query
{

    public record GetMessagesPerChatQuery(Guid userId, Guid ChatId) : IRequest<Response<List<ChatMessageDto>>>;


}
