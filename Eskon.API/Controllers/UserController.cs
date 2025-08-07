using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Core.Features.UserRolesFeatures.Commands.Command;
using Eskon.Domian.DTOs.RolesDTOs;
using Eskon.Domian.DTOs.UserDTOs;
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


        #region GET
        #region Get All users
        /// <summary>
        /// Retrieves a list of all registered users.
        /// </summary>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a list of all users in the system.
        /// </returns>
        /// <remarks>
        /// Accessible without any role restriction. Use for administrative or system-wide views.
        /// </remarks>
        /// <response code="200">Returns the list of users successfully.</response>
        [HttpGet("List")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await Mediator.Send(new GetAllUsersQuery());
            return NewResult(response);
        }
        #endregion

        #region Get user by Id
        /// <summary>
        /// Retrieves a specific user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier (GUID) of the user.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the user details if found; otherwise, a 404 Not Found response.
        /// </returns>
        /// <response code="200">Returns the user details successfully.</response>
        /// <response code="404">User with the specified ID was not found.</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetUserByIdQuery(id));
            return NewResult(response);
        }
        #endregion

        #region Get all Admins
        /// <summary>
        /// Retrieves a paginated list of all users with the "Admin" role.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (starting from 1).</param>
        /// <param name="itemsPerPage">The number of items to include per page.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a paginated list of <see cref="AdminsReadDTO"/> objects.
        /// </returns>
        /// <response code="200">Returns the list of admins successfully.</response>
        /// <response code="401">Unauthorized – user is not authenticated.</response>
        /// <response code="403">Forbidden – user does not have the "Root" role.</response>
        [Authorize(Roles = "Root")]
        [HttpGet("AdminsList")]
        public async Task<IActionResult> GetAllAdmins([FromQuery] int pageNumber, [FromQuery] int itemsPerPage)
        {
            var response = await Mediator.Send(new GetAllAdminsQuery(
                                            itemsPerPage: itemsPerPage,
                                            pageNumber: pageNumber));
            return NewResult(response);
        }  
        #endregion
        #endregion

        [Authorize(Roles = "Customer")]
        // Id of the user to be added to Owner Role
        [HttpPut("AddOwner")]
        public async Task<IActionResult> AddOwnerRole([FromBody]OwnerRoleDTO ownerRoleDTO)
        {
            Guid UserToBeOwnerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new CreateStripeConnectedAccountAndFillLinkCommand(UserToBeOwnerId, ownerRoleDTO));
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
