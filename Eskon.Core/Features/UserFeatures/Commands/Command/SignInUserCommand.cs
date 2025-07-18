
using Eskon.Core.Response;
using Eskon.Domian.DTOs.User;
using Eskon.Domian.Entities.Identity;
using MediatR;

namespace Eskon.Core.Features.UserFeatures.Commands.Command
{
    public record SignInUserCommand(UserSignInDto UserSignInDto) : IRequest<Response<TokenResponseDto>>;
}
