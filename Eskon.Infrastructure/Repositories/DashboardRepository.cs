using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly MyDbContext _context;
        public DashboardRepository(MyDbContext context)
        {
            _context = context;
        }
        public Task<int> CountPendingPropertiesAsync()
        {
            return _context.Properties.CountAsync(p => p.IsSuspended); // موجودة عندك كده
        }

        public Task<int> CountPropertiesAsync()
        {
            return _context.Properties.CountAsync();
        }

        public Task<int> CountAcceptedPropertiesAsync()
        {
            return _context.Properties.CountAsync(p => p.IsAccepted == true);
        }

        public Task<int> CountRejectedPropertiesAsync()
        {
            return _context.Properties.CountAsync(p => p.IsAccepted == false);
        }


        public Task<int> CountUsersByRoleAsync(string role)
        {
            return _context.Users
                .CountAsync(u => u.UserRoles.Any(ur => ur.Role.Name == role));
        }


        public Task<int> CountBookingsAsync()
        {
            return _context.Bookings.CountAsync();
        }

        public Task<int> CountAcceptedBookingsAsync()
        {
            return _context.Bookings.CountAsync(b => b.IsAccepted == true);
        }

        public Task<int> CountPendingBookingsAsync()
        {
            return _context.Bookings.CountAsync(b => b.IsAccepted == null);
        }

        public async Task<Dictionary<string, int>> GetPropertiesByTypeAsync()
        {
            return await _context.Properties
                .GroupBy(p => p.PropertyType.Name)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Type, x => x.Count);
        }

        public async Task<Dictionary<string, decimal>> GetRevenueByMonthAsync()
        {
            return await _context.Payments
                .GroupBy(p => new { p.CreatedAt.Year, p.CreatedAt.Month })
                .Select(g => new
                {
                    Month = g.Key.Year + "-" + g.Key.Month,
                    Revenue = g.Sum(p => p.Fees)
                })
                .ToDictionaryAsync(x => x.Month, x => x.Revenue);
        }
        public async Task<Dictionary<string, int>> GetBookingsByStatusAsync()
        {
            return await _context.Bookings
                .GroupBy(b => b.IsAccepted ? "Accepted" : (b.IsPending ? "Pending" : "Rejected"))
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetPropertiesByStatusAsync()
        {
            return await _context.Properties
                .GroupBy(p =>
                    p.IsSuspended ? "Suspended" :
                    (p.IsPending ? "Pending" :
                    (p.IsAccepted ? "Accepted" : "Rejected"))
                )
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }

    }
}
