using Eskon.Core.Response;
using Eskon.Domian.DTOs.Booking;
using Eskon.Domian.Models;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Commands.Command
{
    public record AddNewBookingCommand(Guid propertyId, BookingWriteDTO bookingWriteDTO) : IRequest<Response<Booking>>;  // Change response to Booking Details/Summary DTO
}
