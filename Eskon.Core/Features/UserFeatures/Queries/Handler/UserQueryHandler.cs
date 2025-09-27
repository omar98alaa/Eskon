using AutoMapper;
using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.UserDTOs;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace Eskon.Core.Features.UserFeatures.Queries.Handler
{
    public class UserQueryHandler : ResponseHandler, IUserQueryHandler
    {
        #region Fields
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserQueryHandler(IServiceUnitOfWork serviceUnitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Handlers
        public async Task<Response<List<UserReadDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var UsersList = await _serviceUnitOfWork.UserService.GetAllUsersAsync();
            var UsersListDto = _mapper.Map<List<UserReadDto>>(UsersList);
            return Success(UsersListDto);
        }

        public async Task<Response<UserReadDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _serviceUnitOfWork.UserService.GetUserByIdAsync(request.id);
            if (user == null)
            {
                return NotFound<UserReadDto>(message: $"User with id {request.id} not found");
            }
            return Success(_mapper.Map<UserReadDto>(user));
        }

        public async Task<Response<Paginated<AdminsReadDTO>>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            var AdminUsersPaginated = await _serviceUnitOfWork.UserService.GetUsersByRolePaginated(pageNumber: request.pageNumber, itemsPerPage: request.itemsPerPage, role: "Admin");
            var AdminsList = _mapper.Map<List<AdminsReadDTO>>(AdminUsersPaginated.Data);
            return Success(new Paginated<AdminsReadDTO>
                (data: AdminsList, 
                pageNumber: AdminUsersPaginated.PageNumber, 
                pageSize: AdminUsersPaginated.PageSize,
                totalRecords: AdminUsersPaginated.TotalRecords
                ));
           
        }
        #endregion
    }
}
