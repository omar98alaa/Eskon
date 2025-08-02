using Eskon.Core.Response;
using Eskon.Domian.DTOs.RefreshTokenDTOs;
using MediatR;

namespace Eskon.Core.Features.AccountFeatures.Commands.Command
{
    public record SignOutUserCommand(CurrentRefreshTokenDTO CurrentRefreshToken, Guid userId) : IRequest<Response<string>>;
}
