using Eskon.Core.Response;
using Eskon.Domian.DTOs.Stripe;
using MediatR;

namespace Eskon.Core.Features.StripeFeatures.Commands.Command
{
    public record CreateStripeCheckoutLinkCommand(CreateStripeCheckoutRequestDTO RequestDTO) : IRequest<Response<string>>;
}
