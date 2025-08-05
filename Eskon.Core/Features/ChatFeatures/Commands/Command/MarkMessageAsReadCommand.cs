using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.ChatFeatures.Commands.Command
{
    public record MarkMessagesAsReadCommand(Guid ChatId, Guid UserId) : IRequest<Unit>;

}
