using Eskon.Core.Response;
using Eskon.Domian.DTOs.Stripe;
using MediatR;

namespace Eskon.Core.Features.StripeFeatures.Commands.Command
{
    public record CreateStripeConnectedAccountLinkForOwnerCommand(CreateStripeConnectedAccountFillLinkRequestDTO RequestDTO) : IRequest<Response<string>>;
}
