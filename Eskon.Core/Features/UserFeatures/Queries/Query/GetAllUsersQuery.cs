using MediatR;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.UserDTOs;

namespace Eskon.Core.Features.UserFeatures.Queries.Query
{
    public record GetAllUsersQuery : IRequest<Response<List<UserReadDto>>>;
}
