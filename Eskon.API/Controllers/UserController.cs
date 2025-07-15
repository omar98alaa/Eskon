using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eskon.Core.Features.UserFeatures.Commands;
using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        #endregion

        #region Controllers
        [HttpPost("/User/AddNew")]
        public async Task<IActionResult> Post([FromBody] UserWriteDto user)
        {
            var userToAdd = _mapper.Map<User>(user);
            var userAdded = await _mediator.Send(new AddUserCommand(userToAdd));
            return Ok(userAdded);
        }

        [HttpGet("/User/List")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _mediator.Send(new GetAllUsersQuery());
            return Ok(response);
        }

        [HttpGet("/User/{id}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(response);
        }

        #endregion

    }
}
