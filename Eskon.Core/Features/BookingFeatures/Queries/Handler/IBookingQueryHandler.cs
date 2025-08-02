using Eskon.Core.Features.BookingFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Booking;
using MediatR;


namespace Eskon.Core.Features.BookingFeatures.Queries.Handler
{
    public interface IBookingQueryHandler : IRequestHandler<GetOwnerBookingsQuery,Response<List<BookingReadDTO>>>,
                                            IRequestHandler<GetCustomerBookingsQuery,Response<List<BookingReadDTO>>>,
                                            IRequestHandler<GetPropertyBookingsQuery,Response<List<BookingReadDTO>>>

    {

    }
}
