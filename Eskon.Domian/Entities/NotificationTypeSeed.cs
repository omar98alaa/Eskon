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
                "AdminPromotion",
                "AdminRemoval",

                // Booking
                "BookingCreated",
                "BookingAccepted",
                "BookingRejected",
                "BookingCancelled",
                "BookingUpdated",

                // Property
                "PropertyCreated",
                "PropertyAccepted",
                "PropertyRejected",

                // Payment
                "PaymentSuccess",
                "PaymentFailed",
                "RefundIssued",
                "PaymentPending",

                // Review
                "ReviewReceived"
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
