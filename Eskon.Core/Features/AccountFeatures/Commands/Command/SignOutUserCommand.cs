using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.AccountFeatures.Commands.Command
{
    public record SignOutUserCommand(string refreshToken) : IRequest<Response<string>>;
}
