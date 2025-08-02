using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.StripeFeatures.Commands.Handler
{
    public interface IStripeCommandHandler : IRequestHandler<CreateStripeAccountCommand, Response<string>>,
                                             IRequestHandler<CreateStripeConnectedAccountLinkForOwnerCommand, Response<string>>,
                                             IRequestHandler<CreateStripeCheckoutLinkCommand, Response<string>>;
}
