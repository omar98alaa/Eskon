using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.AccountFeatures.Commands.Command;
using Eskon.Core.Features.UserRolesFeatures.Utilities;
using Eskon.Domian.DTOs.RefreshToken;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
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

        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] UserRegisterDto user)
        {
            var userToAdd = _mapper.Map<User>(user);
            var userAdded = await Mediator.Send(new AddUserAccountCommand(user));
            return NewResult(userAdded);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> Post([FromBody] UserSignInDto userSignInDto)
        {
            var token = await Mediator.Send(new SignInUserCommand(userSignInDto));
            return NewResult(token);
        }

        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOut([FromBody] CurrentRefreshTokenDTO CurrentRefreshToken)
        {
            return NewResult(await Mediator.Send(new SignOutUserCommand(CurrentRefreshToken)));
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] CurrentRefreshTokenDTO CurrentRefreshToken)
        {
            return NewResult(await Mediator.Send(new GetNewAccessToken(CurrentRefreshToken)));
        }
        #endregion
    }
}
