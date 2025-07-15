using MediatR;
using Template.Core.Response;
using Template.Domian.Entities.Identity;

namespace Template.Core.Features.UserFeatures.Queries.Query
{
    public record GetUserByIdQuery(Guid id) : IRequest<Response<User>>
    {
    }
}
