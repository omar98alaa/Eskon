using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using MediatR;

namespace Eskon.Core.Features.UserFeatures.Queries.Handler
{
    public interface IUserQueryHandler : IRequestHandler<GetAllUsersQuery, Response<List<UserReadDto>>>,
                                         IRequestHandler<GetUserByIdQuery, Response<User>>;
}
