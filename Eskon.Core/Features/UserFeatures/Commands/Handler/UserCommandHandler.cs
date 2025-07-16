using AutoMapper;
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
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UserCommandHandler(IUserService userService, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<Response<UserReadDto?>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.UserRegisteredDto);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.UserRegisteredDto, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<UserReadDto?>(internalErrorMessages);
            }

            var userToAdd = _mapper.Map<User>(request.UserRegisteredDto);
            var result = await _userManager.CreateAsync(userToAdd, request.UserRegisteredDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userToAdd, "Client");
                var userFromDb = await _userService.GetUserByEmail(request.UserRegisteredDto.Email);
                return Created(_mapper.Map<UserReadDto>(userFromDb));
            }
            var dbErrorMessages = result.Errors.Select(r => r.Description).ToList();
            return BadRequest<UserReadDto?>(dbErrorMessages);

        }
    }
}
