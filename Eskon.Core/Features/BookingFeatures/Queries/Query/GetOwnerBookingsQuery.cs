using MediatR;
using Eskon.Domian.DTOs.Booking;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;

namespace Eskon.Core.Features.BookingFeatures.Queries.Query
{
    public record GetOwnerBookingsQuery(Guid OwnerId, string Status, int pageNum, int itemsPerPage) : IRequest<Response<Paginated<BookingReadDTO>>>;
}
