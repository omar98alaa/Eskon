namespace Eskon.Infrastructure.Interfaces
{
    public interface IDashboardRepository
    {
        // Users
        Task<int> CountUsersByRoleAsync(string role);

        // Properties
        Task<int> CountPropertiesAsync();
        Task<int> CountPendingPropertiesAsync();
        Task<int> CountAcceptedPropertiesAsync();
        Task<int> CountRejectedPropertiesAsync();

        // Bookings
        Task<int> CountBookingsAsync();
        Task<int> CountAcceptedBookingsAsync();
        Task<int> CountPendingBookingsAsync();

        // Charts
        Task<Dictionary<string, int>> GetPropertiesByTypeAsync();
        Task<Dictionary<string, decimal>> GetRevenueByMonthAsync();
        Task<Dictionary<string, int>> GetBookingsByStatusAsync();
        Task<Dictionary<string, int>> GetPropertiesByStatusAsync();

    }
}
