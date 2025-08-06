using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Commands.Command
{
    public record SetBookingAsAcceptedCommand(Guid bookingId, Guid ownerId) : IRequest<Response<string>>;
}
