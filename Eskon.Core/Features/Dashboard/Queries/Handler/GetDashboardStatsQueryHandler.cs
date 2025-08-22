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
                    TotalCustomers = await _unitOfWork.UserRepository.CountUsersByRoleAsync("Customer"),
                    TotalOwners = await _unitOfWork.UserRepository.CountUsersByRoleAsync("Owner"),
                    TotalAdmins = await _unitOfWork.UserRepository.CountUsersByRoleAsync("Admin"),

                    TotalProperties = await _unitOfWork.PropertyRepository.CountPropertiesAsync(),
                    PendingProperties = await _unitOfWork.PropertyRepository.CountPendingPropertiesAsync(),
                    AcceptedProperties = await _unitOfWork.PropertyRepository.CountAcceptedPropertiesAsync(),
                    RejectedProperties = await _unitOfWork.PropertyRepository.CountRejectedPropertiesAsync(),

                    TotalBookings = await _unitOfWork.BookingRepository.CountBookingsAsync(),
                    AcceptedBookings = await _unitOfWork.BookingRepository.CountAcceptedBookingsAsync(),
                    PendingBookings = await _unitOfWork.BookingRepository.CountPendingBookingsAsync()
                },
                Charts = new ChartDto
                {
                    PropertiesByType = await _unitOfWork.PropertyRepository.GetPropertiesByTypeAsync(),
                    RevenueByMonth = await _unitOfWork.PaymentRepository.GetRevenueByMonthAsync(),
                    BookingsByStatus = await _unitOfWork.BookingRepository.GetBookingsByStatusAsync(),
                    PropertiesByStatus = await _unitOfWork.PropertyRepository.GetPropertiesByStatusAsync()


                }
            };

            return Success(dto);
        }
    }
}
