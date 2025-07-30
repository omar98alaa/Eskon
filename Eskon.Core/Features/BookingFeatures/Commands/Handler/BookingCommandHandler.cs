using AutoMapper;
using Eskon.Core.Features.BookingFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Core.Features.BookingFeatures.Commands.Handler
{
    public class BookingCommandHandler : ResponseHandler, IBookingCommandHandler
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        #endregion

        #region Constructors
        public BookingCommandHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork)
        {
            _mapper = mapper;
            _serviceUnitOfWork = serviceUnitOfWork;
        }
        #endregion

        #region Handlers
        public async Task<Response<Booking>> Handle(AddNewBookingCommand request, CancellationToken cancellationToken)
        {
            var bookingDTO = request.bookingWriteDTO;

            var validationContext = new ValidationContext(bookingDTO);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(bookingDTO, validationContext, results, true);
            if (!isValid)
            {
                var internalErrorMessages = results.Select(r => r.ErrorMessage).ToList();
                return BadRequest<Booking>(internalErrorMessages);
            }

            // Check property exists
            var property = await _serviceUnitOfWork.PropertyService.GetPropertyByIdAsync(request.propertyId);

            if (property == null)
            {
                return NotFound<Booking>("Property does not exist");
            }

            // Check booking start date is at least 3 days later
            var now = DateOnly.FromDateTime(DateTime.UtcNow);

            if(bookingDTO.StartDate < now.AddDays(3))
            {
                return BadRequest<Booking>("Cannot make reservation less than 3 days ahead");
            }

            // Check property active
            if (!property.IsAccepted || property.IsSuspended)
            {
                return BadRequest<Booking>("Property is not available");
            }

            // Check overlapping with accepted bookings
            var acceptedBookings = await _serviceUnitOfWork.BookingService.GetUpcomingBookingsPerPropertyAsync(property.Id);

            var overlappingBookingsExist = acceptedBookings.Any(b => b.EndDate >= bookingDTO.StartDate && b.StartDate <= bookingDTO.EndDate);

            if (overlappingBookingsExist)
            {
                return BadRequest<Booking>("Reservation period not available");
            }

            // Create pending booking
            var newBooking = new Booking()
            {
                PropertyId = request.propertyId,
                StartDate = bookingDTO.StartDate,
                EndDate = bookingDTO.EndDate
            };

            await _serviceUnitOfWork.BookingService.AddBookingAsync(newBooking);

            return Created(newBooking, message: "Booking request submitted successfully. Awaiting confirmation");
        }

        public async Task<Response<string>> Handle(SetBookingAsAcceptedCommand request, CancellationToken cancellationToken)
        {
            // Check booking exists
            var booking = await _serviceUnitOfWork.BookingService.GetBookingById(request.bookingId);
            if(booking == null)
            {
                return NotFound<string>("Booking does not exist");
            }

            // Check same owner
            if(booking.Property.OwnerId != request.ownerId)
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

            // Check overlapping bookings
            var acceptedBookings = await _serviceUnitOfWork.BookingService.GetUpcomingBookingsPerPropertyAsync(booking.PropertyId);
            var overlappingBookingsExist = acceptedBookings.Any(b => b.EndDate >= booking.StartDate && b.StartDate <= booking.EndDate);

            // Auto reject booking if overlapping
            if (overlappingBookingsExist)
            {
                await _serviceUnitOfWork.BookingService.SetBookingAsRejectedAsync(booking);
                await _serviceUnitOfWork.SaveChangesAsync();

                return BadRequest<string>("Failed operation");
            }

            // Get and reject all overlapping pending bookings
            var pendingBookings = await _serviceUnitOfWork.BookingService.GetPendingBookingsPerPropertyAsync(booking.PropertyId);
            var overlappingPendingBookings = pendingBookings.FindAll(b => b.EndDate >= booking.StartDate && b.StartDate <= booking.EndDate);

            foreach(var pendingBooking in overlappingPendingBookings)
            {
                await _serviceUnitOfWork.BookingService.SetBookingAsRejectedAsync(pendingBooking);
            }

            await _serviceUnitOfWork.SaveChangesAsync();

            return Success($"Booking with ID: {booking.Id} accepted", message: $"Booking with ID: {booking.Id} accepted");
        }
        #endregion
    }
}
