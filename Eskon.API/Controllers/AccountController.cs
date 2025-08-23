using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.AccountFeatures.Commands.Command;
using Eskon.Core.Features.UserRolesFeatures.Utilities;
using Eskon.Domian.DTOs.RefreshTokenDTOs;
using Eskon.Domian.DTOs.UserDTOs;
using Eskon.Domian.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
        #region Register
        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="request">User registration data.</param>
        /// <returns>Returns the created user or validation errors.</returns>
        /// <response code="201">User created successfully</response>
        /// <response code="400">Validation or creation failed</response>
        [HttpPost("Register")]
        [EndpointDescription("Adding new users to Eskon website")]
        [ProducesResponseType(typeof(UserReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserRegisterDto user)
        {
            var userToAdd = _mapper.Map<User>(user);
            var userAdded = await Mediator.Send(new AddUserAccountCommand(user));
            return NewResult(userAdded);
        }
        #endregion

        #region SignIn
        /// <summary>
        /// Authenticates a user and returns JWT access and refresh tokens.
        /// </summary>
        /// <param name="request">User credentials (email and password).</param>
        /// <returns>A JWT access token and refresh token if authentication is successful.</returns>
        /// <response code="200">Authentication successful. Tokens returned.</response>
        /// <response code="400">Authentication failed due to invalid credentials.</response>

        [HttpPost("SignIn")]
        [EndpointDescription("Signing in users to Eskon website")]
        [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserSignInDto userSignInDto)
        {
            var token = await Mediator.Send(new SignInUserCommand(userSignInDto));
            return NewResult(token);
        }
        #endregion

        #region SignOut
        /// <summary>
        /// Signs out the current user by revoking their refresh token.
        /// </summary>
        /// <param name="request">The sign-out request containing the current refresh token.</param>
        /// <returns>A success message if sign-out was successful.</returns>
        /// <response code="200">Sign-out successful. Refresh token revoked.</response>
        /// <response code="400">Invalid token or suspicious sign-out attempt.</response>
        [Authorize]
        [HttpPost("SignOut")]
        [EndpointDescription("Signout users from Eskon website")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignOut([FromBody] CurrentRefreshTokenDTO CurrentRefreshToken)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            return NewResult(await Mediator.Send(new SignOutUserCommand(CurrentRefreshToken, userId)));
        }
        #endregion

        #region Refresh
        /// <summary>
        /// Generates a new access token using a valid refresh token.
        /// </summary>
        /// <param name="request">The refresh token request.</param>
        /// <returns>New access token and the current refresh token.</returns>
        /// <response code="200">New access token generated successfully.</response>
        /// <response code="401">The refresh token is invalid, revoked, or expired.</response>
        [HttpPost("Refresh")]
        [EndpointDescription("Request a new Access token using the valid refresh token")]
        [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken([FromBody] CurrentRefreshTokenDTO CurrentRefreshToken)
        {
            return NewResult(await Mediator.Send(new GetNewAccessToken(CurrentRefreshToken)));
        } 
        #endregion
        #endregion
    }
}
