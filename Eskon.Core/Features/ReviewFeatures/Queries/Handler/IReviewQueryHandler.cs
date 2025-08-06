using Eskon.Core.Features.ReviewFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.ReviewDTOs;
using MediatR;


namespace Eskon.Core.Features.ReviewFeatures.Queries.Handler
{
    public interface IReviewQueryHandler : IRequestHandler<GetReviewsPerCustomerQuery, Response<Paginated<ReviewReadDTO>>>,
                                           IRequestHandler<GetReviewsPerPropertyQuery, Response<Paginated<ReviewReadDTO>>>;
}
