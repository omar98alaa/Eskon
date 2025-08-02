using AutoMapper;
using Eskon.Core.Features.BookingFeatures.Commands.Handler;
using Eskon.Core.Features.BookingFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.BookingDTOs;
using Eskon.Domian.Models;
using Eskon.Service.UnitOfWork;

namespace Eskon.Core.Features.BookingFeatures.Queries.Handler
{
    public class BookingQueryHandler : ResponseHandler, IBookingQueryHandler
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        #endregion

        #region Constructor
        public BookingQueryHandler(IMapper mapper, IServiceUnitOfWork serviceUnitOfWork)
        {
            _mapper = mapper;
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        #endregion

        public async Task<Response<Paginated<BookingReadDTO>>> Handle(GetOwnerBookingsQuery request, CancellationToken cancellationToken)
        {
            Paginated<Booking> bookings;

            switch (request.Status.ToLower())
            {
                case "pending":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedPendingBookingsPerOwnerAsync(request.OwnerId, request.pageNum, request.itemsPerPage);
                    break;
                case "accepted":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedAcceptedBookingsPerOwnerAsync(request.OwnerId, request.pageNum, request.itemsPerPage);
                    break;
                case "paid":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedPaidBookingsPerOwnerAsync(request.OwnerId, request.pageNum, request.itemsPerPage);
                    break;
                case "history":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedBookingHistoryPerOwnerAsync(request.OwnerId, request.pageNum, request.itemsPerPage);
                    break;
                case "rejected":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedRejectedBookingsPerOwnerAsync(request.OwnerId, request.pageNum, request.itemsPerPage);
                    break;
                default:
                    return BadRequest<Paginated<BookingReadDTO>>("Invalid status.");
       
            }

            var bookingsDTO = _mapper.Map<Paginated<BookingReadDTO>>(bookings);
            return Success(bookingsDTO);
        }

        public async Task<Response<Paginated<BookingReadDTO>>> Handle(GetCustomerBookingsQuery request, CancellationToken cancellationToken)
        {
            Paginated<Booking> bookings;

            switch (request.Status.ToLower())
            {
                case "pending":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedPendingBookingsPerCustomerAsync(request.CustomerId, request.pageNum, request.itemsPerPage);
                    break;
                case "accepted":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedAcceptedBookingsPerCustomerAsync(request.CustomerId, request.pageNum, request.itemsPerPage);
                    break;
                case "paid":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedPaidBookingsPerCustomerAsync(request.CustomerId, request.pageNum, request.itemsPerPage);
                    break;
                case "history":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedBookingHistoryPerCustomerAsync(request.CustomerId, request.pageNum, request.itemsPerPage);
                    break;
                case "rejected":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedRejectedBookingsPerCustomerAsync(request.CustomerId, request.pageNum, request.itemsPerPage);
                    break;
                default:
                    return BadRequest<Paginated<BookingReadDTO>>("Invalid status.");
                    break;
            }

            Paginated<BookingReadDTO> bookingsDTO = _mapper.Map<Paginated<BookingReadDTO>>(bookings);
            return Success(bookingsDTO);
        }

        public async Task<Response<Paginated<BookingReadDTO>>> Handle(GetPropertyBookingsQuery request, CancellationToken cancellationToken)
        {
            Paginated<Booking> bookings;
            switch (request.Status.ToLower())
            {
                case "pending":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedPendingBookingsPerPropertyAsync(request.PropertyId, request.pageNum, request.itemsPerPage);
                    break;
                case "accepted":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedAcceptedBookingsPerPropertyAsync(request.PropertyId, request.pageNum, request.itemsPerPage);
                    break;
                case "paid":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedPaidBookingsPerPropertyAsync(request.PropertyId, request.pageNum, request.itemsPerPage);
                    break;
                case "history":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedBookingHistoryPerPropertyAsync(request.PropertyId, request.pageNum, request.itemsPerPage);
                    break;
                case "rejected":
                    bookings = await _serviceUnitOfWork.BookingService.GetPaginatedRejectedBookingsPerPropertyAsync(request.PropertyId, request.pageNum, request.itemsPerPage);
                    break;
                default:
                    return BadRequest<Paginated<BookingReadDTO>>("Invalid status.");
                    break;
            }

            Paginated<BookingReadDTO> bookingsDTO = _mapper.Map<Paginated<BookingReadDTO>>(bookings);
            return Success(bookingsDTO);
        }
        
    }
}

