using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.StripeFeatures.Commands.Command
{
    public record CreateStripeAccountCommand(Guid userId) : IRequest<Response<string>>;
}
