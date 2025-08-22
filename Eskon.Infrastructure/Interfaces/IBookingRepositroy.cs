using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IBookingRepository : IGenericRepositoryAsync<Booking>
    {
        public Task<bool> CheckBookingExists(Booking booking);
        Task<int> CountBookingsAsync();
        Task<int> CountAcceptedBookingsAsync();
        Task<int> CountPendingBookingsAsync();
        Task<Dictionary<string, int>> GetBookingsByStatusAsync();
    }
}
