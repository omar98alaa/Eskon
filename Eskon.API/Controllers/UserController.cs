using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Eskon.Core.Features.UserFeatures.Commands;
using Eskon.Core.Features.UserFeatures.Queries.Query;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Eskon.API.Base;

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

        #endregion

    }
}
