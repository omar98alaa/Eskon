using Eskon.Domian.Models;
namespace Eskon.Service.Interfaces
{
    public interface IReviewService
    {
        public Task<List<Review>> GetPropertyReviewsAsync(Guid propId);

        public Task<Review> CreatePropertyReviewAsync(Review review);

        public Task UpdatePropertyReviewAsync(Review review);

        public Task DeletePropertyReviewAsync(Review review);

    }
}
