using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.ReviewDTOs;
using MediatR;

namespace Eskon.Core.Features.ReviewFeatures.Queries.Query
{
    public record GetReviewsPerPropertyQuery(Guid PropertyId, int pageNum, int itemsPerPage) : IRequest<Response<Paginated<ReviewReadDTO>>>;
}
