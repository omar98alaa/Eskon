using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Commands.Command
{
    public record MarkMessagesAsReadCommand(Guid ChatId, Guid UserId) : IRequest<Response<Unit>>;

}
