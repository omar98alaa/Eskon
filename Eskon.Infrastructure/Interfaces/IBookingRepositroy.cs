using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IBookingRepository : IGenericRepositoryAsync<Booking>
    {
        public Task<bool> CheckBookingExists(Booking booking);
        public Task<int> GetPendingBookingsCountPerOwnerAsync(Guid OwnerId);
        public Task<int> CountBookingsAsync();
        public Task<int> CountAcceptedBookingsAsync();
        public Task<int> CountPendingBookingsAsync();
        public Task<Dictionary<string, int>> GetBookingsByStatusAsync();
    }
}
