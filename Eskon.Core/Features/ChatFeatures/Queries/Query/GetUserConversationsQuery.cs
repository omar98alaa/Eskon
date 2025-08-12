using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Queries.Query
{
    public record GetUserConversationsQuery(Guid UserId) : IRequest<Response<List<ConversationDto>>>;
}
