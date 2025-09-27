using System.ComponentModel.DataAnnotations;
using Eskon.Domian.Models;

namespace Eskon.Domian.Entities
{
    public class NotificationOutboxMessage : BaseModel
    {
        [Required]
        public string Payload { get; set; } // JSON or string content of the notification

        [Required]
        public string Type { get; set; } // e.g. "Notification", "Email", etc.

        [Required]
        public string Status { get; set; } // e.g. "Pending", "Sent", "Failed"

        public string? Error { get; set; } // Error message if sending failed

        public DateTime? LastTriedAt { get; set; } // Last time sending was attempted
    }
} 