using Eskon.Domian.Models;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Domian.Entities
{
    public static class NotificationTypeSeeder
    {
        public static async Task SeedAsync(DbContext context)
        {
            string[] notificationTypes =
            {
                "Admin Promotion",
                "Admin Removal",

                // Booking
                "Booking Created",
                "Booking Accepted",
                "Booking Rejected",
                "Booking Cancelled",
                "Booking Updated",

                // Property
                "Property Created",
                "Property Accepted",
                "Property Rejected",

                // Payment
                "Payment Success",
                "Payment Failed",
                "Refund Issued",
                "Refund Canceled",
                "Payment Pending",

                // Review
                "Review Received"
            };

            foreach (var typeName in notificationTypes)
            {
                var exists = await context.Set<NotificationType>()
                    .AnyAsync(x => x.Name == typeName);

                if (!exists)
                {
                    context.Set<NotificationType>().Add(new NotificationType
                    {
                        Name = typeName,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
