using Eskon.Domian.DTOs.Chat;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Queries.Query
{

    public record GetMessagesPerChatQuery(Guid ChatId) : IRequest<List<ChatMessageDto>>;


}
