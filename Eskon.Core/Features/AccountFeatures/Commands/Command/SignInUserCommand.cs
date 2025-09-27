using Eskon.Core.Response;
using Eskon.Domian.DTOs.UserDTOs;
using MediatR;

namespace Eskon.Core.Features.AccountFeatures.Commands.Command
{
    public record SignInUserCommand(UserSignInDto UserSignInDto) : IRequest<Response<TokenResponseDto>>;
}
