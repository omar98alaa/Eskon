using AutoMapper;
using Eskon.Core.Features.BookingFeatures.Commands.Handler;
using Eskon.Core.Features.BookingFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Booking;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;

namespace Eskon.Core.Features.BookingFeatures.Queries.Handler
{
    public class BookingQueryHandler : ResponseHandler, IBookingQueryHandler
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        HashSet<string> validStatuses = new HashSet<string> { "pending", "accepted", "paid", "history", "rejected" };

        #endregion

        #region Constructor
        public BookingQueryHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork)
        {
            _mapper = mapper;
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        #endregion

        public async Task<Response<List<BookingReadDTO>>> Handle(GetOwnerBookingsQuery request, CancellationToken cancellationToken)
        {
            if (!validStatuses.Contains(request.Status.ToLower()))
                return BadRequest<List<BookingReadDTO>>("Invalid status.");
            List<Booking> bookings = request.Status.ToLower() switch
            {
                "pending"   =>  await _serviceUnitOfWork.BookingService.GetPendingBookingsPerOwnerAsync(request.OwnerId),
                "accepted"  =>  await _serviceUnitOfWork.BookingService.GetAcceptedBookingsPerOwnerAsync(request.OwnerId),
                "paid"      =>  await _serviceUnitOfWork.BookingService.GetPaidBookingsPerOwnerAsync(request.OwnerId),
                "history"   =>  await _serviceUnitOfWork.BookingService.GetBookingHistoryPerOwnerAsync(request.OwnerId),
                "rejected"  =>  await _serviceUnitOfWork.BookingService.GetRejectedBookingsPerOwnerAsync(request.OwnerId)
            };
            List<BookingReadDTO> bookingsDTO = _mapper.Map<List<BookingReadDTO>>(bookings);
            return Success(bookingsDTO);
        }
        public async Task<Response<List<BookingReadDTO>>> Handle(GetCustomerBookingsQuery request, CancellationToken cancellationToken)
        {
            if (!validStatuses.Contains(request.Status.ToLower()))
                return BadRequest<List<BookingReadDTO>>("Invalid status.");
            List<Booking> bookings = request.Status.ToLower() switch
            {
                "pending" => await _serviceUnitOfWork.BookingService.GetPendingBookingsPerCustomerAsync(request.CustomerId),
                "accepted" => await _serviceUnitOfWork.BookingService.GetAcceptedBookingsPerCustomerAsync(request.CustomerId),
                "paid" => await _serviceUnitOfWork.BookingService.GetPaidBookingsPerCustomerAsync(request.CustomerId),
                "history" => await _serviceUnitOfWork.BookingService.GetBookingHistoryPerCustomerAsync(request.CustomerId),
                "rejected" => await _serviceUnitOfWork.BookingService.GetRejectedBookingsPerCustomerAsync(request.CustomerId)
            };
            List<BookingReadDTO> bookingsDTO = _mapper.Map<List<BookingReadDTO>>(bookings);
            return Success(bookingsDTO);
        }
        public async Task<Response<List<BookingReadDTO>>> Handle(GetPropertyBookingsQuery request, CancellationToken cancellationToken)
        {
            if (!validStatuses.Contains(request.Status.ToLower()))
                return BadRequest<List<BookingReadDTO>>("Invalid status.");

            List<Booking> bookings = request.Status.ToLower() switch
            {
                "pending" => await _serviceUnitOfWork.BookingService.GetPendingBookingsPerPropertyAsync(request.PropertyId),
                "accepted" => await _serviceUnitOfWork.BookingService.GetAcceptedBookingsPerPropertyAsync(request.PropertyId),
                "paid" => await _serviceUnitOfWork.BookingService.GetPaidBookingsPerPropertyAsync(request.PropertyId),
                "history" => await _serviceUnitOfWork.BookingService.GetBookingHistoryPerPropertyAsync(request.PropertyId),
                "rejected" => await _serviceUnitOfWork.BookingService.GetRejectedBookingsPerPropertyAsync(request.PropertyId)
            };
            List<BookingReadDTO> bookingsDTO = _mapper.Map<List<BookingReadDTO>>(bookings);
            return Success(bookingsDTO);
        }

    }
}

