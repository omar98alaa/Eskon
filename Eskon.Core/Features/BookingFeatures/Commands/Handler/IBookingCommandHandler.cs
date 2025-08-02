using Eskon.Core.Features.BookingFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.BookingDTOs;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Commands.Handler
{
    public interface IBookingCommandHandler : IRequestHandler<AddNewBookingCommand, Response<BookingReadDTO>>,
                                              IRequestHandler<SetBookingAsAcceptedCommand, Response<string>>,
                                              IRequestHandler<SetBookingAsRejectedCommand, Response<string>>,
                                              IRequestHandler<CancelBookingCommand, Response<string>>;
}
