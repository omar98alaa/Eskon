using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Queries.Query
{
    public record GetLastReceivedMessageQuery(Guid UserId, Guid chat) : IRequest<Response<ChatMessageDto>>;
}
