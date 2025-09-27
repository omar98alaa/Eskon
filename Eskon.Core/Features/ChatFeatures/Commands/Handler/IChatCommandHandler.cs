using Eskon.Core.Features.ChatFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using MediatR;

namespace Eskon.Core.Features.ChatFeatures.Commands.Handler
{
    interface IChatCommandHandler : IRequestHandler<SendMessageCommand, ChatMessageDto?>,
                                    IRequestHandler<MarkMessagesAsReadCommand, Response<Unit>>,
                                    IRequestHandler<AddNewChatCommand, Response<ConversationDto>>
    {
    }
}
