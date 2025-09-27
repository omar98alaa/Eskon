using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Commands.Command
{
    public record CancelBookingCommand(Guid bookingId, Guid customerId) : IRequest<Response<string>>;
}
