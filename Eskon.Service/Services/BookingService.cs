using Eskon.Domain.Utilities;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class BookingService : IBookingService
    {

        #region Fields
        private readonly IBookingRepository _bookingRepository;
        #endregion

        #region Constructors
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        #endregion

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            await _bookingRepository.AddAsync(booking);
            return booking;
        }

        public async Task SetBookingAsAcceptedAsync(Booking booking)
        {
            booking.IsAccepted = true;
            booking.IsPending = true;
            booking.IsPayed = false;
            await _bookingRepository.UpdateAsync(booking);
        }
        public async Task SetBookingAsRejectedAsync(Booking booking)
        {
            booking.IsAccepted = false;
            booking.IsPending = false;
            booking.IsPayed = false;
            await _bookingRepository.UpdateAsync(booking);
        }
        public async Task SetBookingAsPayedAsync(Booking booking)
        {
            booking.IsPayed = true;
            booking.IsPending = false;
            booking.IsAccepted = true;
            await _bookingRepository.UpdateAsync(booking);
        }
        public async Task SetBookingAsRefundedAsync(Booking booking)
        {
            booking.IsPayed = false;
            booking.IsAccepted = true;
            booking.IsPending = false;
            await _bookingRepository.UpdateAsync(booking);
        }
        public async Task SetBookingAsCancelledAsync(Booking booking)
        {
            booking.IsPending = false;
            booking.IsAccepted = true;
            booking.IsPayed = false;
            await _bookingRepository.UpdateAsync(booking);
        }

        public async Task SoftRemoveBookingAsync(Booking booking)
        {
            await _bookingRepository.SoftDeleteAsync(booking);
        }

        public async Task RemoveBookingAsync(Booking booking)
        {
            await _bookingRepository.DeleteAsync(booking);
        }

        public async Task RemoveBookingRangeAsync(List<Booking> bookings)
        {
            await _bookingRepository.DeleteRangeAsync(bookings);
        }

        public async Task<Booking> GetBookingById(Guid Id)
        {
            return await _bookingRepository.GetByIdAsync(Id);
        }
        public async Task<Paginated<Booking>> GetPaginatedBookingHistoryPerCustomerAsync(Guid customerId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.CustomerId == customerId && b.StartDate <= now && !b.IsPending && b.IsPayed);
        }

        public async Task<Paginated<Booking>> GetPaginatedAcceptedBookingsPerCustomerAsync(Guid customerId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.CustomerId == customerId && b.StartDate > now && b.IsAccepted);
        }

        public async Task<Paginated<Booking>> GetPaginatedPaidBookingsPerCustomerAsync(Guid customerId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.CustomerId == customerId && b.StartDate > now && b.IsPayed);
        }

        public async Task<Paginated<Booking>> GetPaginatedPendingBookingsPerCustomerAsync(Guid customerId, int pageNum, int itemsPerPage)
        {
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.CustomerId == customerId && b.IsPending && !b.IsAccepted);
        }

        public async Task<Paginated<Booking>> GetPaginatedRejectedBookingsPerCustomerAsync(Guid customerId, int pageNum, int itemsPerPage)
        {
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.CustomerId == customerId && !b.IsPending && !b.IsAccepted);
        }

        public async Task<Paginated<Booking>> GetPaginatedPendingBookingsPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.Property.OwnerId == ownerId && b.StartDate > now && b.IsPending);
        }

        public async Task<Paginated<Booking>> GetPaginatedAcceptedBookingsPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.Property.OwnerId == ownerId && b.StartDate > now && b.IsPending);
        }

        public async Task<Paginated<Booking>> GetPaginatedPaidBookingsPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.Property.OwnerId == ownerId && b.StartDate > now && b.IsPayed);
        }

        public async Task<Paginated<Booking>> GetPaginatedBookingHistoryPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.Property.OwnerId == ownerId && b.StartDate <= now && !b.IsPending && b.IsPayed);
        }

        public async Task<Paginated<Booking>> GetPaginatedRejectedBookingsPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.Property.OwnerId == ownerId && !b.IsPending && !b.IsAccepted);
        }

        public async Task<Paginated<Booking>> GetPaginatedAcceptedBookingsPerPropertyAsync(Guid propertyId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.PropertyId == propertyId && b.StartDate > now && b.IsAccepted);
        }

        public async Task<Paginated<Booking>> GetPaginatedPendingBookingsPerPropertyAsync(Guid propertyId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.PropertyId == propertyId && b.StartDate > now && b.IsPending);
        }

        public async Task<Paginated<Booking>> GetPaginatedBookingHistoryPerPropertyAsync(Guid propertyId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.PropertyId == propertyId && b.StartDate <= now && !b.IsPending && b.IsPayed);
        }

        public async Task<Paginated<Booking>> GetPaginatedPaidBookingsPerPropertyAsync(Guid propertyId, int pageNum, int itemsPerPage)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.PropertyId == propertyId && b.StartDate > now && b.IsPayed);
        }

        public async Task<Paginated<Booking>> GetPaginatedRejectedBookingsPerPropertyAsync(Guid propertyId, int pageNum, int itemsPerPage)
        {
            return await _bookingRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: b => b.PropertyId == propertyId && !b.IsPending && !b.IsAccepted);
        }

        public async Task<List<Booking>> GetAcceptedBookingsPerPropertyAsync(Guid propertyId)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetFilteredAsync(b => b.PropertyId == propertyId && b.StartDate > now && b.IsAccepted);
        }

        public async Task<List<Booking>> GetPendingBookingsPerPropertyAsync(Guid propertyId)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetFilteredAsync(b => b.PropertyId == propertyId && b.StartDate > now && b.IsPending);
        }

        public async Task<bool> IsAlreadyBookedBefore(Booking newBooking)
        {
            return await _bookingRepository.CheckBookingExists(newBooking);
        }

        public async Task<List<Booking>> GetUnpaidPassedAcceptedDateBookingsAsync()
        {
            var dateNow = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetFilteredAsync(
                   b => b.IsPayed == false && 
                        b.StartDate.AddDays(-1) <= dateNow);
        }

        public async Task<int> GetPendingBookingsCountPerOwnerAsync(Guid OwnerId)
        {
            return await _bookingRepository.GetPendingBookingsCountPerOwnerAsync(OwnerId);
        }
    }
}
