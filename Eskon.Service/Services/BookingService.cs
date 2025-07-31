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

        public async Task<Booking> GetBookingById(Guid Id)
        {
            return await _bookingRepository.GetByIdAsync(Id);
        }


        public async Task<List<Booking>> GetBookingHistoryPerCustomerAsync(Guid customerId)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetFilteredAsync(b => b.UserId == customerId && b.StartDate <= now && !b.IsPending && b.IsPayed);
        }

        public async Task<List<Booking>> GetBookingHistoryPerPropertyAsync(Guid propertyId)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetFilteredAsync(b => b.PropertyId == propertyId && b.StartDate <= now && !b.IsPending && b.IsPayed);
        }

        public async Task<List<Booking>> GetAcceptedBookingsPerCustomerAsync(Guid customerId)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetFilteredAsync(b => b.UserId == customerId && b.StartDate > now && b.IsAccepted);
        }

        public async Task<List<Booking>> GetPayedBookingsPerCustomerAsync(Guid customerId)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetFilteredAsync(b => b.UserId == customerId && b.StartDate > now && b.IsPayed);
        }

        public async Task<List<Booking>> GetPendingBookingsPerCustomerAsync(Guid customerId)
        {
            return await _bookingRepository.GetFilteredAsync(b => b.UserId == customerId && b.IsPending && !b.IsAccepted);
        }

        public async Task<List<Booking>> GetPendingBookingsPerOwnerAsync(Guid ownerId)
        {
            var now = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _bookingRepository.GetFilteredAsync(b => b.Property.OwnerId == ownerId && b.StartDate > now && b.IsPending);
        }

        public async Task<List<Booking>> GetRejectedBookingsPerCustomerAsync(Guid customerId)
        {
            return await _bookingRepository.GetFilteredAsync(b => b.UserId == customerId && !b.IsPending && !b.IsAccepted);
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
    }
}
