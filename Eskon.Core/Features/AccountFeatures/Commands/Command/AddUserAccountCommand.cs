using MediatR;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.UserDTOs;

namespace Eskon.Core.Features.AccountFeatures.Commands.Command
{
    public record AddUserAccountCommand(UserRegisterDto UserRegisterDto) : IRequest<Response<UserReadDto>>;
}
