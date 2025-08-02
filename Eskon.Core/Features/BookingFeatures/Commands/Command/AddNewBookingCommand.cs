using Eskon.Core.Response;
using Eskon.Domian.DTOs.BookingDTOs;
using Eskon.Domian.Models;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Commands.Command
{
    public record AddNewBookingCommand(Guid propertyId, BookingRequestDTO bookingRequestDTO) : IRequest<Response<Booking>>;  // Change response to Booking Details/Summary DTO
}
