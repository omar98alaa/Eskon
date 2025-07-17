using MediatR;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Core.Features.UserFeatures.Commands
{
    public record AddUserCommand(UserRegisterDto UserRegisterDto) : IRequest<Response<UserReadDto>>;
}
