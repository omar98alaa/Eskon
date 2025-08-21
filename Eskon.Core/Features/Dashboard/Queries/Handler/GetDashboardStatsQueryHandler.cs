using Eskon.Core.Features.Dashboard.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Dashboard;
using Eskon.Infrastructure.UnitOfWork;

namespace Eskon.Core.Features.Dashboard.Queries.Handler
{
    public class GetDashboardStatsQueryHandler : ResponseHandler, IGetDashboardStatsQueryHandler
    {
        private readonly IRepositoryUnitOfWork _unitOfWork;

        public GetDashboardStatsQueryHandler(IRepositoryUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<DashboardDto>> Handle(GetDashboardStateQuery request, CancellationToken cancellationToken)
        {
            var dto = new DashboardDto
            {
                KPIs = new KpiDto
                {
                    TotalCustomers = await _unitOfWork.DashboardRepository.CountUsersByRoleAsync("Customer"),
                    TotalOwners = await _unitOfWork.DashboardRepository.CountUsersByRoleAsync("Owner"),
                    TotalAdmins = await _unitOfWork.DashboardRepository.CountUsersByRoleAsync("Admin"),

                    TotalProperties = await _unitOfWork.DashboardRepository.CountPropertiesAsync(),
                    PendingProperties = await _unitOfWork.DashboardRepository.CountPendingPropertiesAsync(),
                    AcceptedProperties = await _unitOfWork.DashboardRepository.CountAcceptedPropertiesAsync(),
                    RejectedProperties = await _unitOfWork.DashboardRepository.CountRejectedPropertiesAsync(),

                    TotalBookings = await _unitOfWork.DashboardRepository.CountBookingsAsync(),
                    AcceptedBookings = await _unitOfWork.DashboardRepository.CountAcceptedBookingsAsync(),
                    PendingBookings = await _unitOfWork.DashboardRepository.CountPendingBookingsAsync()
                },
                Charts = new ChartDto
                {
                    PropertiesByType = await _unitOfWork.DashboardRepository.GetPropertiesByTypeAsync(),
                    RevenueByMonth = await _unitOfWork.DashboardRepository.GetRevenueByMonthAsync(),
                    BookingsByStatus = await _unitOfWork.DashboardRepository.GetBookingsByStatusAsync(),
                    PropertiesByStatus = await _unitOfWork.DashboardRepository.GetPropertiesByStatusAsync()


                }
            };

            return Success(dto);
        }
    }
}
