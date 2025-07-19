using MediatR;
using Eskon.Core.Response;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Core.Features.UserFeatures.Queries.Query
{
    public record GetUserByIdQuery(Guid id) : IRequest<Response<User>>;
}
