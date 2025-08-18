using Eskon.Domain.Utilities;
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

        public Task<Paginated<Booking>> GetPaginatedPendingBookingsPerCustomerAsync(Guid customerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedPendingBookingsPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedAcceptedBookingsPerCustomerAsync(Guid customerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedAcceptedBookingsPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedRejectedBookingsPerCustomerAsync(Guid customerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedRejectedBookingsPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedPaidBookingsPerCustomerAsync(Guid customerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedPaidBookingsPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedBookingHistoryPerCustomerAsync(Guid customerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedBookingHistoryPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedBookingHistoryPerPropertyAsync(Guid propertyId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedAcceptedBookingsPerPropertyAsync(Guid propertyId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedPendingBookingsPerPropertyAsync(Guid propertyId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedRejectedBookingsPerPropertyAsync(Guid propertyId, int pageNum, int itemsPerPage);
        public Task<Paginated<Booking>> GetPaginatedPaidBookingsPerPropertyAsync(Guid propertyId, int pageNum, int itemsPerPage);
        public Task<List<Booking>> GetAcceptedBookingsPerPropertyAsync(Guid propertyId);
        public Task<List<Booking>> GetPendingBookingsPerPropertyAsync(Guid propertyId);

        public Task<List<Booking>> GetUnpaidPassedAcceptedDateBookingsAsync();

        public Task<bool> IsAlreadyBookedBefore(Booking newBooking);
        public Task<List<Booking>> GetPendingBookingsPerOwnerAsync(Guid OwnerId);


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
        public Task RemoveBookingRangeAsync(List<Booking> bookings);
        #endregion
    }

}
