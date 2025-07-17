using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Service.Interfaces;
using Eskon.Core.Features.UserFeatures.Commands.Command;
using Eskon.Domian.Entities.Identity;
using Microsoft.AspNetCore.Identity;


namespace Eskon.Core.Features.UserFeatures.Commands.Handler
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<UserReadDto>>
        , IRequestHandler<SignInUserCommand, Response<string>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly SignInManager<User> _signInManager;


        public UserCommandHandler(IUserService userService, IMapper mapper, IAuthenticationService authenticationService, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, SignInManager<User> signInManager)
        {
            _userService = userService;
            _mapper = mapper;
            _authenticationService = authenticationService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        #region AddUserCommand Handler
        public async Task<Response<UserReadDto?>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.UserRegisterDto);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.UserRegisterDto, validationContext, results, true);
            if (!isValid)
            {
                var errorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<UserReadDto?>(errorMessages);
            }

            var userToAdd = _mapper.Map<User>(request.UserRegisterDto);
            var result = await _userManager.CreateAsync(userToAdd, request.UserRegisterDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userToAdd, "Customer");
                var userFromDb = await _userService.GetUserByEmailAsync(request.UserRegisterDto.Email);
                return Created(_mapper.Map<UserReadDto>(userFromDb));
            }
            var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
            return BadRequest<UserReadDto?>(dbErrorMessages);

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
                return BadRequest<string>("Incorrect email or password");
            }

            var accessToken = await _authenticationService.GetJWTToken(user);
            return Success(accessToken);
        } 
        #endregion
    }
}
