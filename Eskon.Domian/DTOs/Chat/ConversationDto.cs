namespace Eskon.Domian.DTOs.Chat
{
    public class ConversationDto
    {
        public Guid Id { get; set; }
        public string User1Id { get; set; }
        public string User1Name { get; set; }
        public string User2Id { get; set; }
        public string User2Name { get; set; }
        public string? LastMessage { get; set; }
        public DateTime? LastMessageTime { get; set; }
        public int UnreadCount { get; set; }
    }
}
