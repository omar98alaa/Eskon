using Eskon.Domain.Utilities;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    class ReviewService : IReviewService
    {
        #region Fields
        private readonly IReviewRepository _reviewRepository;
        #endregion

        #region Constructors
        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        #endregion

        #region Methods

        public async Task<Review?> GetReviewByIdAsync(Guid reviewId)
        {
            return await _reviewRepository.GetByIdAsync(reviewId);
        }


        public async Task<Review> CreatePropertyReviewAsync(Review review)
        {

            return await _reviewRepository.AddAsync(review);
        }


        public async Task<List<Review>> GetPropertyReviewsAsync(Guid propId)
        {
            return await _reviewRepository.GetFilteredAsync(f => f.PropertyId == propId);
        }

        public async Task<Paginated<Review>> GetPropertyReviewsPaginatedAsync(Guid propertyId, int pageNumber, int itemsPerPage) 
        {
            return await _reviewRepository.GetPaginatedSortedAsync(r => r.CreatedAt, false, filter: r => r.PropertyId == propertyId, pageNumber: pageNumber, itemsPerPage: itemsPerPage);
        }

        public async Task<Paginated<Review>> GetCustomerReviewsPaginatedAsync(Guid customerId, int pageNumber, int itemsPerPage) 
        { 
            return await _reviewRepository.GetPaginatedSortedAsync(r => r.CreatedAt, false, filter: r => r.CustomerId == customerId, pageNumber: pageNumber, itemsPerPage: itemsPerPage);
        }

        public async Task UpdatePropertyReviewAsync(Review review)
        {
            review.UpdatedAt = DateTime.UtcNow;
            await _reviewRepository.UpdateAsync(review);
        }


        public async Task DeletePropertyReviewAsync(Review review)
        {
            await _reviewRepository.DeleteAsync(review);
        }

        #endregion
    }
}
