using Eskon.Domian.DTOs.Chat;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Commands.Command
{

    public record SendMessageCommand(SendMessageDto MessageDto) : IRequest<ChatMessageDto>;

}
