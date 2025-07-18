using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Eskon.Core.Features.UserFeatures.Commands;
using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Eskon.API.Base;
using Eskon.Core.Features.UserFeatures.Commands.Command;
using Microsoft.AspNetCore.Authorization;

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


        [HttpGet("/User/List")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await Mediator.Send(new GetAllUsersQuery());
            return NewResult(response);
        }

        [HttpGet("/User/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetUserByIdQuery(id));
            return NewResult(response);
        }

        [Authorize(Roles = "Customer, Admin")]
        // Id of the user to be added to Owner Role
        [HttpPut("/User/AddOwner/{UserToBeOwnerId:guid}")]
        public async Task<IActionResult> AddOwnerRole([FromRoute] Guid UserToBeOwnerId)
        {
            var response = await Mediator.Send(new AddOwnerRoleToUserCommand(UserToBeOwnerId));
            return NewResult(response);
        }

        [Authorize(Roles = "Admin")]
        // Id of the user to be removed from Owner Role
        [HttpPut("/User/RemoveOwner/{UserToRemoveOwnerId:guid}")] 
        public async Task<IActionResult> DeleteOwnerRole([FromRoute] Guid UserToRemoveOwnerId)
        {
            var response = await Mediator.Send(new DeleteOwnerRoleFromUserCommand(UserToRemoveOwnerId));
            return NewResult(response);
        }

        [Authorize(Roles = "Root")]
        // Id of the user to be added to Admin Role
        [HttpPut("/User/AddAdmin/{UserToBeAdminId:guid}")]
        public async Task<IActionResult> AddAdminRole([FromRoute] Guid UserToBeAdminId)
        {
            var response = await Mediator.Send(new AddAdminRoleToUserCommand(UserToBeAdminId));
            return NewResult(response);
        }

        [Authorize(Roles = "Root")]
        // Id of the user to be removed from Admin Role
        [HttpPut("/User/RemoveAdmin/{UserToRemoveAdminId:guid}")]
        public async Task<IActionResult> DeleteAdminRole([FromRoute] Guid UserToRemoveAdminId)
        {
            var response = await Mediator.Send(new DeleteAdminRoleFromUserCommand(UserToRemoveAdminId));
            return NewResult(response);
        }

        #endregion

    }
}
