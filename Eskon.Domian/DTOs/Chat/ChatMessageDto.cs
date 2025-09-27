namespace Eskon.Domian.DTOs.Chat
{
    public class ChatMessageDto
    {
        public Guid MessageId { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set;}
        public bool IsRead => ReadAt.HasValue;
    }
}
