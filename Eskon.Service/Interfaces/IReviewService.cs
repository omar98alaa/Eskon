using Eskon.Domain.Utilities;
using Eskon.Domian.Models;
namespace Eskon.Service.Interfaces
{
    public interface IReviewService
    {
        public Task<Review?> GetReviewByIdAsync(Guid reviewId);
        public Task<List<Review>> GetPropertyReviewsAsync(Guid propId);

        public Task<Paginated<Review>> GetPropertyReviewsPaginatedAsync(Guid propertyId, int pageNumber, int itemsPerPage);
        
        public Task<Paginated<Review>> GetCustomerReviewsPaginatedAsync(Guid customerId, int pageNumber, int itemsPerPage);

        public Task<Review> CreatePropertyReviewAsync(Review review);

        public Task UpdatePropertyReviewAsync(Review review);

        public Task DeletePropertyReviewAsync(Review review);

    }
}
