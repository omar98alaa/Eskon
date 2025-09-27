using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Commands.Command
{
    public record SetBookingAsRejectedCommand(Guid bookingId, Guid ownerId) : IRequest<Response<string>>;
}
