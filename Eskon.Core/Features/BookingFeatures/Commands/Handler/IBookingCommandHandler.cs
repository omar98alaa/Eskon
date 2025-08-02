using Eskon.Core.Features.BookingFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.Models;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Commands.Handler
{
    public interface IBookingCommandHandler : IRequestHandler<AddNewBookingCommand, Response<Booking>>,   // Change to Booking Details/Summary DTO 
                                              IRequestHandler<SetBookingAsAcceptedCommand, Response<string>>,
                                              IRequestHandler<SetBookingAsRejectedCommand, Response<string>>,
                                              IRequestHandler<CancelBookingCommand, Response<string>>;
}
