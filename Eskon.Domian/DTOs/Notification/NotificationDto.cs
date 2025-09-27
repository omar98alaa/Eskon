namespace Eskon.Domian.DTOs.Notification
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        // Notification Type
        public string NotificationTypeName { get; set; }

        // Redirection
        public Guid? RedirectionId { get; set; }
        public string RedirectionName { get; set; }
    }
} 