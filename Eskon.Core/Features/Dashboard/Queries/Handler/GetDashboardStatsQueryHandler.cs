using Eskon.Core.Features.Dashboard.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Dashboard;
using Eskon.Infrastructure.UnitOfWork;
using Eskon.Service.UnitOfWork;

namespace Eskon.Core.Features.Dashboard.Queries.Handler
{
    public class GetDashboardStatsQueryHandler : ResponseHandler, IGetDashboardStatsQueryHandler
    {
        private readonly IServiceUnitOfWork _unitOfWork;

        public GetDashboardStatsQueryHandler(IServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<DashboardDto>> Handle(GetDashboardStateQuery request, CancellationToken cancellationToken)
        {
            var dto = new DashboardDto
            {
                KPIs = new KpiDto
                {
                    TotalCustomers = await _unitOfWork.UserService.CountUsersByRoleAsync("Customer"),
                    TotalOwners = await _unitOfWork.UserService.CountUsersByRoleAsync("Owner"),
                    TotalAdmins = await _unitOfWork.UserService.CountUsersByRoleAsync("Admin"),

                    TotalProperties = await _unitOfWork.PropertyService.CountPropertiesAsync(),
                    PendingProperties = await _unitOfWork.PropertyService.CountPendingPropertiesAsync(),
                    AcceptedProperties = await _unitOfWork.PropertyService.CountAcceptedPropertiesAsync(),
                    RejectedProperties = await _unitOfWork.PropertyService.CountRejectedPropertiesAsync(),

                    TotalBookings = await _unitOfWork.BookingService.CountBookingsAsync(),
                    AcceptedBookings = await _unitOfWork.BookingService.CountAcceptedBookingsAsync(),
                    PendingBookings = await _unitOfWork.BookingService.CountPendingBookingsAsync()
                },
                Charts = new ChartDto
                {
                    PropertiesByType = await _unitOfWork.PropertyService.GetPropertiesByTypeAsync(),
                    RevenueByMonth = await _unitOfWork.PaymentService.GetRevenueByMonthAsync(),
                    BookingsByStatus = await _unitOfWork.BookingService.GetBookingsByStatusAsync(),
                    PropertiesByStatus = await _unitOfWork.PropertyService.GetPropertiesByStatusAsync()


                }
            };

            return Success(dto);
        }
    }
}
