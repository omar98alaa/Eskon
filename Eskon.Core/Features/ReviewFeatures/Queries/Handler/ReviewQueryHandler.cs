using AutoMapper;
using Eskon.Core.Features.ReviewFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.ReviewDTOs;
using Eskon.Service.UnitOfWork;

namespace Eskon.Core.Features.ReviewFeatures.Queries.Handler
{
    public class ReviewQueryHandler : ResponseHandler, IReviewQueryHandler
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        #endregion

        #region Constructor
        public ReviewQueryHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork)
        {
            _mapper = mapper;
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        #endregion

        public async Task<Response<Paginated<ReviewReadDTO>>> Handle(GetReviewsPerCustomerQuery request, CancellationToken cancellationToken)
        {
            var paginatedReviews = await _serviceUnitOfWork.ReviewService.GetCustomerReviewsPaginatedAsync(request.CustomerId, request.pageNum, request.itemsPerPage);
            var reviewsDTO = _mapper.Map<List<ReviewReadDTO>>(paginatedReviews.Data);
            var paginatedReviewsDTO = new Paginated<ReviewReadDTO>(
                reviewsDTO,
                paginatedReviews.PageNumber,
                paginatedReviews.PageSize,
                paginatedReviews.TotalRecords
            );

            return Success(paginatedReviewsDTO);
        }

        public async Task<Response<Paginated<ReviewReadDTO>>> Handle(GetReviewsPerPropertyQuery request, CancellationToken cancellationToken)
        {
            var paginatedReviews = await _serviceUnitOfWork.ReviewService.GetPropertyReviewsPaginatedAsync(request.PropertyId, request.pageNum, request.itemsPerPage);
            var reviewsDTO = _mapper.Map<List<ReviewReadDTO>>(paginatedReviews.Data);
            var paginatedReviewsDTO = new Paginated<ReviewReadDTO>(
                reviewsDTO,
                paginatedReviews.PageNumber,
                paginatedReviews.PageSize,
                paginatedReviews.TotalRecords
            );

            return Success(paginatedReviewsDTO);
        }
    }
}

