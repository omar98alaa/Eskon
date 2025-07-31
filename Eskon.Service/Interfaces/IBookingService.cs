using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IBookingService
    {
        #region Create
        public Task<Booking> AddBookingAsync(Booking booking);
        #endregion

        #region Read
        public Task<Booking> GetBookingById(Guid Id);

        public Task<List<Booking>> GetPendingBookingsPerOwnerAsync(Guid ownerId);
        public Task<List<Booking>> GetPendingBookingsPerCustomerAsync(Guid customerId);
        public Task<List<Booking>> GetAcceptedBookingsPerCustomerAsync(Guid customerId);
        public Task<List<Booking>> GetRejectedBookingsPerCustomerAsync(Guid customerId);
        public Task<List<Booking>> GetPayedBookingsPerCustomerAsync(Guid customerId);
        public Task<List<Booking>> GetBookingHistoryPerCustomerAsync(Guid customerId);
        public Task<List<Booking>> GetBookingHistoryPerPropertyAsync(Guid propertyId);
        public Task<List<Booking>> GetAcceptedBookingsPerPropertyAsync(Guid propertyId);
        public Task<List<Booking>> GetPendingBookingsPerPropertyAsync(Guid propertyId);
        #endregion

        #region Update
        public Task SetBookingAsAcceptedAsync(Booking booking);
        public Task SetBookingAsRejectedAsync(Booking booking);
        public Task SetBookingAsPayedAsync(Booking booking);
        public Task SetBookingAsRefundedAsync(Booking booking);
        public Task SetBookingAsCancelledAsync(Booking booking);
        #endregion

        #region Delete
        public Task SoftRemoveBookingAsync(Booking booking);
        public Task RemoveBookingAsync(Booking booking);
        #endregion
    }

}
