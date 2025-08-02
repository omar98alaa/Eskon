using Eskon.Core.Response;
using Eskon.Domian.DTOs.Booking;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Queries.Query
{
   public record GetPropertyBookingsQuery(Guid PropertyId, string Status) : IRequest<Response<List<BookingReadDTO>>>;
}
