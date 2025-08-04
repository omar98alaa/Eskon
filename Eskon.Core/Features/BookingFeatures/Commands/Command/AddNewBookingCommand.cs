using Eskon.Core.Response;
using Eskon.Domian.DTOs.BookingDTOs;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Commands.Command
{
    public record AddNewBookingCommand(Guid customerId, BookingRequestDTO bookingRequestDTO) : IRequest<Response<BookingReadDTO>>;
}
