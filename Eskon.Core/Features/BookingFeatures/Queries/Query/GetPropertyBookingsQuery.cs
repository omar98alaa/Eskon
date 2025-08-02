using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Booking;
using MediatR;

namespace Eskon.Core.Features.BookingFeatures.Queries.Query
{
   public record GetPropertyBookingsQuery(Guid PropertyId, string Status, int pageNum, int itemsPerPage) : IRequest<Response<Paginated<BookingReadDTO>>>;
}
