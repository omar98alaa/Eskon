using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Core.Features.UserRolesFeatures.Commands.Command;
using Eskon.Domian.DTOs.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region Controllers


        [HttpGet("List")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await Mediator.Send(new GetAllUsersQuery());
            return NewResult(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetUserByIdQuery(id));
            return NewResult(response);
        }

        [Authorize(Roles = "Customer")]
        // Id of the user to be added to Owner Role
        [HttpPut("AddOwner")]
        public async Task<IActionResult> AddOwnerRole()
        {
            Guid UserToBeOwnerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new AddOwnerRoleToUserCommand(UserToBeOwnerId));
            return NewResult(response);
        }

        [Authorize(Roles = "Root")]
        // Id of the user to be added to Admin Role
        [HttpPut("AddAdmin")]
        public async Task<IActionResult> AddAdminRole([FromBody] AdminRoleDTO AdminRole)
        {
            var response = await Mediator.Send(new AddAdminRoleToUserCommand(AdminRole));
            return NewResult(response);
        }

        [Authorize(Roles = "Root")]
        // Id of the user to be removed from Admin Role
        [HttpPut("RemoveAdmin")]
        public async Task<IActionResult> DeleteAdminRole([FromBody] AdminRoleDTO AdminRole)
        {
            var response = await Mediator.Send(new DeleteAdminRoleFromUserCommand(AdminRole));
            return NewResult(response);
        }

        #endregion

    }
}
