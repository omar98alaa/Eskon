using MediatR;
using Eskon.Core.Response;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.DTOs.User;

namespace Eskon.Core.Features.UserFeatures.Queries.Query
{
    public record GetUserByIdQuery(Guid id) : IRequest<Response<UserReadDto>>;
}
