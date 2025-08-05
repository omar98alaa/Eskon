namespace Eskon.Domian.DTOs.Chat
{
    public class SendMessageDto
    {
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
    }

}
