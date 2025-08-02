using MediatR;
using Eskon.Domian.DTOs.Booking;
using Eskon.Core.Response;

namespace Eskon.Core.Features.BookingFeatures.Queries.Query
{
    public record GetCustomerBookingsQuery(Guid CustomerId, String Status) : IRequest<Response<List<BookingReadDTO>>>;
}
