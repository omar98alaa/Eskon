using AutoMapper;
using MediatR;
using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.Interfaces;

namespace Eskon.Core.Features.UserFeatures.Queries.Handler
{
    public class UserQueryHandler : ResponseHandler,
                                    IRequestHandler<GetAllUsersQuery, Response<List<UserReadDto>>>,
                                        IRequestHandler<GetUserByIdQuery, Response<User>>
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public UserQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<List<UserReadDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var UsersList = await _userService.GetAllUsersAsync();
            var UsersListDto = _mapper.Map<List<UserReadDto>>(UsersList);
            return Success(UsersListDto);
        }

        public async Task<Response<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.id);
            if (user == null)
            {
                return NotFound<User>(message: $"Student with id {request.id} not found");
            }
            return Success(user);
        }
        #endregion
    }
}
