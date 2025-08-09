using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Commands.Command
{
    public record AddNewChatCommand(Guid user1Id, Guid user2Id) : IRequest<Response<ConversationDto>>;
}
