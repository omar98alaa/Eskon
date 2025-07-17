using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Service.Interfaces;
using Eskon.Core.Features.UserFeatures.Commands.Command;
using Eskon.Domian.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using Eskon.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Eskon.Core.Features.UserFeatures.Commands.Handler
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<UserReadDto>>
        , IRequestHandler<SignInUserCommand, Response<string>>
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly SignInManager<User> _signInManager;
        #endregion

        #region Constructors
        public UserCommandHandler(IUserService userService, IMapper mapper, IAuthenticationService authenticationService, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, SignInManager<User> signInManager)
        {
            _userService = userService;
            _mapper = mapper;
            _authenticationService = authenticationService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        #endregion

        #region AddUserCommand Handler
        public async Task<Response<UserReadDto?>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.UserRegisterDto);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.UserRegisterDto, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<UserReadDto?>(internalErrorMessages);
            }

            var userToAdd = _mapper.Map<User>(request.UserRegisteredDto);
            var result = await _userManager.CreateAsync(userToAdd, request.UserRegisteredDto.Password);
            if (!result.Succeeded)
            {
                var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
                return BadRequest<UserReadDto?>(dbErrorMessages);
            }
            await _userManager.AddToRoleAsync(userToAdd, "Customer");
            var userFromDb = await _userService.GetUserByEmail(request.UserRegisteredDto.Email);
            return Created(_mapper.Map<UserReadDto>(userFromDb));
            var userToAdd = _mapper.Map<User>(request.UserRegisterDto);
            var result = await _userManager.CreateAsync(userToAdd, request.UserRegisterDto.Password);
            if (!result.Succeeded)
            {
                var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
                return BadRequest<UserReadDto?>(dbErrorMessages);
            }
            await _userManager.AddToRoleAsync(userToAdd, "Client");
            var userFromDb = await _userService.GetUserByEmailAsync(request.UserRegisterDto.Email);
            return Created(_mapper.Map<UserReadDto>(userFromDb));

        }
        #endregion

        #region SignInUserCommand Handler
        public async Task<Response<string>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserSignInDto.Email);

            if (user == null)
            {
                return BadRequest<string>("The email you provided is not registered before, create new account");
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.UserSignInDto.Password, false);
             
            if(!signInResult.Succeeded)
            {
                return BadRequest<string>("Wrong Passsword");
            }

            var accessToken = await _authenticationService.GetJWTToken(user);
            return Success(accessToken);
        } 
        #endregion
    }
}
