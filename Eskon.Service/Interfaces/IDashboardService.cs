using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Service.Interfaces
{
    public interface IDashboardService
    {
        Task<int> CountUsersByRoleAsync(string role);
        Task<int> CountPropertiesAsync();
        Task<int> CountPendingPropertiesAsync();
        Task<Dictionary<string, int>> GetPropertiesByTypeAsync();
        Task<Dictionary<string, decimal>> GetRevenueByMonthAsync();
    }
}
