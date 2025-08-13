using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Core.Features.UserRolesFeatures.Commands.Command;
using Eskon.Core.Response;
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

        #region PUT Roles
        #region Add Owner Role / Create stripe account
        /// <summary>
        /// Assigns the "Owner" role to the authenticated user and initiates the creation of a Stripe connected account.
        /// </summary>
        /// <param name="ownerRoleDTO">
        /// The details required to create the Stripe connected account, provided in the request body.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a link to complete the Stripe account setup process.
        /// </returns>
        /// <response code="201">The Stripe connected account was successfully created and linked to the user.</response>
        /// <response code="400">Bad request – user already has an active Stripe account or invalid input was provided.</response>
        /// <response code="401">Unauthorized – the request lacks valid authentication credentials.</response>
        /// <response code="403">Forbidden – the authenticated user does not have the "Customer" role.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">An internal server error occurred while creating the Stripe account.</response>
        [Authorize(Roles = "Customer")]
        [HttpPut("AddOwner")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOwnerRole([FromBody] OwnerRoleDTO ownerRoleDTO)
        {
            Guid UserToBeOwnerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new CreateStripeConnectedAccountAndFillLinkCommand(UserToBeOwnerId, ownerRoleDTO));
            return NewResult(response);
        }
        #endregion

        #region Add Admin Role
        /// <summary>
        /// Assigns the "Admin" role to an existing user based on their email address.
        /// </summary>
        /// <param name="AdminRole">
        /// The DTO containing the email address of the user who should be assigned the "Admin" role.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a success message or an error message if the operation fails.
        /// </returns>
        /// <response code="200">User successfully assigned the "Admin" role and a new access token issued.</response>
        /// <response code="400">Bad request – validation failed or there were errors assigning the role.</response>
        /// <response code="401">Unauthorized – the request lacks valid authentication credentials.</response>
        /// <response code="403">Forbidden – the authenticated user does not have the "Root" role.</response>
        /// <response code="404">User not found.</response>
        [Authorize(Roles = "Root")]
        // Id of the user to be added to Admin Role
        [HttpPut("AddAdmin")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddAdminRole([FromBody] AdminRoleDTO AdminRole)
        {
            var response = await Mediator.Send(new AddAdminRoleToUserCommand(AdminRole));
            return NewResult(response);
        }
        #endregion

        #region Remove Admin Role
        /// <summary>
        /// Removes the "Admin" role from a specified user.
        /// </summary>
        /// <param name="AdminRole">
        /// The data transfer object containing the email of the user whose "Admin" role should be removed.
        /// </param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a success message if the role was removed successfully,
        /// or an error message if the operation failed.
        /// </returns>
        /// <remarks>
        /// This endpoint can only be accessed by users with the "Root" role.  
        /// The process:
        /// <list type="number">
        /// <item>Validates the provided email address.</item>
        /// <item>Looks up the user by email.</item>
        /// <item>Removes the "Admin" role from the user.</item>
        /// <item>Returns a success message if successful, or an error if the operation fails.</item>
        /// </list>
        /// </remarks>
        /// <response code="200">The "Admin" role was successfully removed from the user.</response>
        /// <response code="400">Bad request – validation failed or the role removal operation was unsuccessful.</response>
        /// <response code="401">Unauthorized – the request lacks valid authentication credentials.</response>
        /// <response code="403">Forbidden – the authenticated user does not have the "Root" role.</response>
        /// <response code="404">The specified user was not found.</response>
        [Authorize(Roles = "Root")]
        [HttpPut("RemoveAdmin")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAdminRole([FromBody] AdminRoleDTO AdminRole)
        {
            var response = await Mediator.Send(new DeleteAdminRoleFromUserCommand(AdminRole));
            return NewResult(response);
        }  
        #endregion
        #endregion

        #endregion

    }
}
