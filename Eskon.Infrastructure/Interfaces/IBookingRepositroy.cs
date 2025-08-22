using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IBookingRepository : IGenericRepositoryAsync<Booking>
    {
        public Task<bool> CheckBookingExists(Booking booking);
        public Task<int> GetPendingBookingsCountPerOwnerAsync(Guid OwnerId);
    }
}
