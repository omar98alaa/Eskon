using Azure;
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


        public async Task<Review> CreatePropertyReviewAsync(Review review)
        {

            return await _reviewRepository.AddAsync(review);
        }



        public async Task<List<Review>> GetPropertyReviewsAsync(Guid propId)
        {
            return await _reviewRepository.GetFilteredAsync(f => f.PropertyId == propId);
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



        public async Task<int> SaveChangesAsync()
        {
            return await _reviewRepository.SaveChangesAsync();
        }

        #endregion
    }
}
