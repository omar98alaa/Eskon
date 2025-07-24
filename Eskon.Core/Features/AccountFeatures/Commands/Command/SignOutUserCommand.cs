using Eskon.Core.Response;
using Eskon.Domian.DTOs.RefreshToken;
using MediatR;

namespace Eskon.Core.Features.AccountFeatures.Commands.Command
{
    public record SignOutUserCommand(CurrentRefreshTokenDTO CurrentRefreshToken) : IRequest<Response<string>>;
}
