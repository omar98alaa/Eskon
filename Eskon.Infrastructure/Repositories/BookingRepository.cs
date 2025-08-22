using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepositoryAsync<Booking>, IBookingRepository
    {
        #region Fields
        private readonly DbSet<Booking> _bookingDbSet;
        #endregion

        #region Constructors
        public BookingRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _bookingDbSet = myDbContext.Set<Booking>();
        }
        #endregion

        #region Methods
        public async Task<bool> CheckBookingExists(Booking booking)
        {
            return await _bookingDbSet.AnyAsync(b =>
                b.CustomerId == booking.CustomerId &&
                b.StartDate == booking.StartDate &&
                b.EndDate == booking.EndDate &&
                b.PropertyId == booking.PropertyId
            );
        }

        public async Task<int> GetPendingBookingsCountPerOwnerAsync(Guid OwnerId)
        {
            return await _bookingDbSet.Where(b => b.IsPending == true && b.Property.OwnerId == OwnerId).CountAsync();
        }
        #endregion
    }
}
