using AutoMapper;
using Eskon.Core.Features.BookingFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.BookingDTOs;
using Eskon.Domian.Models;
using Eskon.Service.Interfaces;
using Eskon.Service.UnitOfWork;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Core.Features.BookingFeatures.Commands.Handler
{
    public class BookingCommandHandler : ResponseHandler, IBookingCommandHandler
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly IEmailService _emailService;
        #endregion

        #region Constructors
        public BookingCommandHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork, IEmailService emailService)
        {
            _mapper = mapper;
            _serviceUnitOfWork = serviceUnitOfWork;
            _emailService = emailService;
        }
        #endregion

        #region Helpers
        bool IsOverlappingBooking(Booking booking, DateOnly startDate, DateOnly endDate, Guid bookingId)
        {
            return booking.Id != bookingId && booking.EndDate >= startDate && booking.StartDate <= endDate;
        }

        bool IsOverlappingBooking(Booking booking, DateOnly startDate, DateOnly endDate)
        {
            return booking.EndDate >= startDate && booking.StartDate <= endDate;
        }
        #endregion

        #region Handlers
        public async Task<Response<BookingReadDTO>> Handle(AddNewBookingCommand request, CancellationToken cancellationToken)
        {
            var bookingRequestDTO = request.bookingRequestDTO;

            var validationContext = new ValidationContext(bookingRequestDTO);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(bookingRequestDTO, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<BookingReadDTO>(internalErrorMessages);
            }

            // Check property exists
            var property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.bookingRequestDTO.PropertyId);

            if (property == null)
            {
                return NotFound<BookingReadDTO>("Property does not exist");
            }

            // Check property active
            if (!property.IsAccepted || property.IsSuspended)
            {
                return BadRequest<BookingReadDTO>("Property is not available");
            }

            // Check dates are valid
            if (bookingRequestDTO.StartDate >= bookingRequestDTO.EndDate)
            {
                return BadRequest<BookingReadDTO>("Start date cannot be greater than or equal to end date");
            }

            // Check booking start date is at least 3 days later
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (bookingRequestDTO.StartDate < today.AddDays(3))
            {
                return BadRequest<BookingReadDTO>("Cannot make a reservation less than 3 days ahead");
            }

            // Map to new booking
            var newBooking = _mapper.Map<Booking>(bookingRequestDTO);
            
            // Set customer Id
            newBooking.CustomerId = request.customerId;

            // Check if it is a duplicate booking
            if (await _serviceUnitOfWork.BookingService.IsAlreadyBookedBefore(newBooking))
            {
                return BadRequest<BookingReadDTO>("Booking is processing");
            }

            // Get number of days
            var days = bookingRequestDTO.EndDate.DayNumber - bookingRequestDTO.StartDate.DayNumber;

            // Set total price
            newBooking.TotalPrice = property.PricePerNight * days;

            // Check overlapping with accepted bookings
            var acceptedBookings = await _serviceUnitOfWork.BookingService.GetAcceptedBookingsPerPropertyAsync(property.Id);

            var overlappingBookingsExist = acceptedBookings.Any(b => IsOverlappingBooking(b, bookingRequestDTO.StartDate, bookingRequestDTO.EndDate, ));

            if (overlappingBookingsExist)
            {
                return BadRequest<BookingReadDTO>("Reservation period not available");
            }

            // Add pending booking request
            await _serviceUnitOfWork.BookingService.AddBookingAsync(newBooking);
            await _serviceUnitOfWork.SaveChangesAsync();

            var bookingDTO = _mapper.Map<BookingReadDTO>(newBooking);

            return Created(bookingDTO, message: "Booking request submitted successfully. Awaiting confirmation");
        }

        public async Task<Response<string>> Handle(SetBookingAsAcceptedCommand request, CancellationToken cancellationToken)
        {
            // Check booking exists
            var booking = await _serviceUnitOfWork.BookingService.GetBookingById(request.bookingId);
            if (booking == null)
            {
                return NotFound<string>("Booking does not exist");
            }

            // Check same owner
            if (booking.Property.OwnerId != request.ownerId)
            {
                return Forbidden<string>();
            }

            // Check if booking is not pending
            if (!booking.IsPending)
            {
                return BadRequest<string>("Booking already processed");
            }

            // Set booking as accepted and save it
            await _serviceUnitOfWork.BookingService.SetBookingAsAcceptedAsync(booking);
            await _serviceUnitOfWork.SaveChangesAsync();

            // Check overlapping bookings (To avoid race conditions)
            var acceptedBookings = await _serviceUnitOfWork.BookingService.GetAcceptedBookingsPerPropertyAsync(booking.PropertyId);
            var overlappingBookingsExist = acceptedBookings.Any(b => IsOverlappingBooking(b, booking.StartDate, booking.EndDate, booking.Id));

            // Auto reject booking if overlapping
            if (overlappingBookingsExist)
            {
                await _serviceUnitOfWork.BookingService.SetBookingAsRejectedAsync(booking);
                await _serviceUnitOfWork.SaveChangesAsync();

                return BadRequest<string>("Overlapping with another booking");
            }



            // Get and reject all overlapping pending bookings
            var pendingBookings = await _serviceUnitOfWork.BookingService.GetPendingBookingsPerPropertyAsync(booking.PropertyId);
            var overlappingPendingBookings = pendingBookings.FindAll(b => IsOverlappingBooking(b, booking.StartDate, booking.EndDate, booking.Id));

            foreach (var pendingBooking in overlappingPendingBookings)
            {
                await _serviceUnitOfWork.BookingService.SetBookingAsRejectedAsync(pendingBooking);
            }



            _emailService.SendEmailAsync(
                booking.Customer.Email,
                "ESKON: Booking request accepted",
                $"Hello Mr.{booking.Customer.LastName}\n\n" +
                $"Your booking request for: {booking.Property.Title}\n" +
                $"From: {booking.StartDate} To: {booking.EndDate}\n" +
                $"has been accepted.\n" +
                $"Please login before {booking.StartDate.AddDays(-1)} to pay and confirm your booking or it will be automatically cancelled."
            );

            return Success($"Booking with ID: {booking.Id} accepted", message: $"Booking with ID: {booking.Id} accepted");
        }

        public async Task<Response<string>> Handle(SetBookingAsRejectedCommand request, CancellationToken cancellationToken)
        {
            // Check booking exists
            var booking = await _serviceUnitOfWork.BookingService.GetBookingById(request.bookingId);
            if (booking == null)
            {
                return NotFound<string>("Booking does not exist");
            }

            // Check same owner
            if (booking.Property.OwnerId != request.ownerId)
            {
                return Forbidden<string>();
            }

            // Check if booking is not pending
            if (!booking.IsPending)
            {
                return BadRequest<string>("Booking already processed");
            }

            // Set booking as rejected and save it
            await _serviceUnitOfWork.BookingService.SetBookingAsRejectedAsync(booking);

            await _serviceUnitOfWork.SaveChangesAsync();

            return Success($"Booking with ID: {booking.Id} rejected", message: $"Booking with ID: {booking.Id} rejected");
        }

        public async Task<Response<string>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            // Check booking exists
            var booking = await _serviceUnitOfWork.BookingService.GetBookingById(request.bookingId);
            if (booking == null)
            {
                return NotFound<string>("Booking does not exist");
            }

            // Check same customer
            if (booking.CustomerId != request.customerId)
            {
                return Forbidden<string>();
            }

            // Check if booking is payed
            if (booking.IsPayed)
            {
                return BadRequest<string>("Booking already paid");
            }

            // Delete booking
            await _serviceUnitOfWork.BookingService.RemoveBookingAsync(booking);

            await _serviceUnitOfWork.SaveChangesAsync();

            return Success($"Booking with ID: {booking.Id} removed", message: $"Booking with ID: {booking.Id} removed");
        }
        #endregion
    }
}
