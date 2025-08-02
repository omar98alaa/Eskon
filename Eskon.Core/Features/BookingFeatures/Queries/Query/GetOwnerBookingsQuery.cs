using MediatR;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.BookingDTOs;

namespace Eskon.Core.Features.BookingFeatures.Queries.Query
{
    public record GetOwnerBookingsQuery(Guid OwnerId, string Status, int pageNum, int itemsPerPage) : IRequest<Response<Paginated<BookingReadDTO>>>;
}
