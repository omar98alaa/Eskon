using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Commands.Command
{
    public record SendMessageCommand(Guid senderId, SendMessageDto MessageDto) : IRequest<ChatMessageDto?>;
}
