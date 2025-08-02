using Eskon.Core.Response;
using Eskon.Domian.DTOs.BookingDTOs;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Commands.Command
{
    public record AddNewBookingCommand(Guid propertyId, BookingRequestDTO bookingRequestDTO) : IRequest<Response<BookingReadDTO>>;
}
