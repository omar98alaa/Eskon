using MediatR;
using Template.Core.Response;
using Template.Domian.DTOs.User;

namespace Template.Core.Features.UserFeatures.Queries.Query
{
    public record GetAllUsersQuery : IRequest<Response<List<UserReadDto>>>
    {
    }
}
