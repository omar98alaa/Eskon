namespace Eskon.Domian.DTOs.Dashboard
{
    public class DashboardDto
    {
        public KpiDto KPIs { get; set; }
        public ChartDto Charts { get; set; }
    }

    public class KpiDto
    {
        // Users
        public int TotalCustomers { get; set; }
        public int TotalOwners { get; set; }
        public int TotalAdmins { get; set; }

        // Properties
        public int TotalProperties { get; set; }
        public int PendingProperties { get; set; }
        public int AcceptedProperties { get; set; }
        public int RejectedProperties { get; set; }

        // Bookings
        public int TotalBookings { get; set; }
        public int AcceptedBookings { get; set; }
        public int PendingBookings { get; set; }
    }

    public class ChartDto
    {
        public Dictionary<string, int> PropertiesByType { get; set; }
        public Dictionary<string, decimal> RevenueByMonth { get; set; }
        public Dictionary<string, int> BookingsByStatus { get; set; }
        public Dictionary<string, int> PropertiesByStatus { get; set; }
    
}
}
