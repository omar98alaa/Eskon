using AutoMapper;
using Azure.Core;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Models;
using Eskon.Service.Interfaces;


namespace Eskon.Core.Features.UserFeatures.Commands.Handler
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<UserReadDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        public async Task<Response<UserReadDto?>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext(request.User);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request.User, validationContext, results, true);
            if (!isValid)
            {
                var errorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<UserReadDto?>(errorMessages);
            }

            var addedUser = await _userService.AddUserAsync(request.User);
            await _userService.SaveChangesAsync();
            var userReadDto = _mapper.Map<UserReadDto>(addedUser);
            return Created(userReadDto);
        }
    }
}
