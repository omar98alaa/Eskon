using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IChatMessageRepository : IGenericRepositoryAsync<ChatMessage>
    {
        Task<List<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId);
        Task AddMessageAsync(ChatMessage message);
        Task<List<ConversationDto>> GetConversationsForUserAsync(Guid userId);
        Task MarkMessagesAsReadAsync(Guid chatId, Guid userId);

    }
}
