using Eskon.Infrastructure.Interfaces;
using Eskon.Infrastructure.UnitOfWork;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class DashboardService : IDashboardService
    {
       private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
        public DashboardService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }

        public async Task<int> CountAcceptedBookingsAsync()
        {
            return await _repositoryUnitOfWork.BookingRepository.CountAcceptedBookingsAsync();
        }

        public async Task<int> CountPendingBookingsAsync()
        {
            return await _repositoryUnitOfWork.BookingRepository.CountPendingBookingsAsync();
        }

        public async Task<int> CountPendingPropertiesAsync()
        {
            return await _repositoryUnitOfWork.PropertyRepository.CountPendingPropertiesAsync();

        }

        public async Task<int> CountPropertiesAsync()
        {
            return await _repositoryUnitOfWork.PropertyRepository.CountPropertiesAsync();
        }

        public async Task<int> CountUsersByRoleAsync(string role)
        {
            return await _repositoryUnitOfWork.UserRepository.CountUsersByRoleAsync(role);
        }

        public async Task<Dictionary<string, int>> GetBookingsByStatusAsync()
        {
            return await _repositoryUnitOfWork.BookingRepository.GetBookingsByStatusAsync();
        }

        public async Task<Dictionary<string, int>> GetPropertiesByTypeAsync()
        {
            return await _repositoryUnitOfWork.PropertyRepository.GetPropertiesByTypeAsync();
        }

        public async Task<Dictionary<string, decimal>> GetRevenueByMonthAsync()
        {
            return await _repositoryUnitOfWork.PaymentRepository.GetRevenueByMonthAsync();
        }

    }
}
