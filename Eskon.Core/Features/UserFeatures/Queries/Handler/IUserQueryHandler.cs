using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.UserDTOs;
using MediatR;

namespace Eskon.Core.Features.UserFeatures.Queries.Handler
{
    public interface IUserQueryHandler : IRequestHandler<GetAllUsersQuery, Response<List<UserReadDto>>>,
                                         IRequestHandler<GetUserByIdQuery, Response<UserReadDto>>,
                                         IRequestHandler<GetAllAdminsQuery, Response<Paginated<AdminsReadDTO>>>;
}
