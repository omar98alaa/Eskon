using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Service.Services
{
    public class DashboardService : IDashboardService
    {
        public readonly IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<int> CountPendingPropertiesAsync()
        {
            return await _dashboardRepository.CountPendingPropertiesAsync();

        }

        public async Task<int> CountPropertiesAsync()
        {
            return await _dashboardRepository.CountPropertiesAsync();
        }

        public async Task<int> CountUsersByRoleAsync(string role)
        {
            return await _dashboardRepository.CountUsersByRoleAsync(role);
        }

        public async Task<Dictionary<string, int>> GetPropertiesByTypeAsync()
        {
            return await _dashboardRepository.GetPropertiesByTypeAsync();
        }

        public async Task<Dictionary<string, decimal>> GetRevenueByMonthAsync()
        {
            return await _dashboardRepository.GetRevenueByMonthAsync();
        }
    }
}
