using Eskon.Domian.DTOs.Chat;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.ChatFeatures.Queries.Query
{
    public record GetUserConversationsQuery(Guid UserId) : IRequest<List<ConversationDto>>;
}
