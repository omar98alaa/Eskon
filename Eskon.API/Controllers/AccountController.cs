using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.UserFeatures.Commands;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public AccountController(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Actions

        [HttpPost("/Account/Register")]
        public async Task<IActionResult> Post([FromBody] UserRegisterDto user)
        {
            var userToAdd = _mapper.Map<User>(user);
            var userAdded = await Mediator.Send(new AddUserCommand(user));
            return NewResult(userAdded);
        }

        #endregion
    }
}
