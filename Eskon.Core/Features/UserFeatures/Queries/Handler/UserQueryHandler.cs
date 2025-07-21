using AutoMapper;
using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.UnitOfWork;

namespace Eskon.Core.Features.UserFeatures.Queries.Handler
{
    public class UserQueryHandler : ResponseHandler, IUserQueryHandler
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public UserQueryHandler(IServiceUnitOfWork serviceUnitOfWork, IMapper mapper)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<List<UserReadDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var UsersList = await _serviceUnitOfWork.UserService.GetAllUsersAsync();
            var UsersListDto = _mapper.Map<List<UserReadDto>>(UsersList);
            return Success(UsersListDto);
        }

        public async Task<Response<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _serviceUnitOfWork.UserService.GetUserByIdAsync(request.id);
            if (user == null)
            {
                return NotFound<User>(message: $"Student with id {request.id} not found");
            }
            return Success(user);
        }
        #endregion
    }
}
