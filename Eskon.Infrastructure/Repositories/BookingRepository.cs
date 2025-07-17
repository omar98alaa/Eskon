using Microsoft.EntityFrameworkCore;
using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Eskon.Domian.Models;

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

        #endregion
    }
}
