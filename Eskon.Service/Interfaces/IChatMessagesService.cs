using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IChatMessagesService
    {
        Task<ChatMessage> AddMessageAsync(ChatMessage chatMessage);
        Task<List<ChatMessage>> GetMessagesPerChatAsync(Chat chat);
        Task<ChatMessage?> GetMessageByIdAsync(Guid messageId);
        Task UpdateMessageAsync(ChatMessage chatMessage);
        Task DeleteMessageAsync(ChatMessage chatMessage);
        Task MarkMessagesAsReadAsync(Guid chatId, Guid userId);

    }
}
