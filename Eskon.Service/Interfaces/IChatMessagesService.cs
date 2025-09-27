using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IChatMessagesService
    {
        Task<ChatMessage> AddMessageAsync(ChatMessage chatMessage);
        Task<List<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId);
        Task UpdateMessageAsync(ChatMessage chatMessage);
        Task DeleteMessageAsync(ChatMessage chatMessage);
        Task MarkMessagesAsRead(Chat chat, Guid userId);
        Task<ChatMessage?> GetlastMessagesAsync(Guid chat, Guid userId);
    }
}
