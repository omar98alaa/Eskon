using Eskon.Core.Features.BookingFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Booking;
using MediatR;


namespace Eskon.Core.Features.BookingFeatures.Queries.Handler
{
    public interface IBookingQueryHandler : IRequestHandler<GetOwnerBookingsQuery,Response<Paginated<BookingReadDTO>>>,
                                            IRequestHandler<GetCustomerBookingsQuery,Response<Paginated<BookingReadDTO>>>,
                                            IRequestHandler<GetPropertyBookingsQuery,Response<Paginated<BookingReadDTO>>>

    {

    }
}
