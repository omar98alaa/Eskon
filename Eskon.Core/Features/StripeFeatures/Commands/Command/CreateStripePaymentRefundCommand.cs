using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.StripeFeatures.Commands.Command
{
    public record CreateStripePaymentRefundCommand(Guid CustomerId, Guid BookingId) : IRequest<Response<string>>;
}
