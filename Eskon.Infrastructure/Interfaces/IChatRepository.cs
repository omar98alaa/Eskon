using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IChatRepository : IGenericRepositoryAsync<Chat>
    {
        Task<List<Chat>> GetChatsForUserAsync(Guid userId);
        Task<bool> ChatExistsAsync(Guid user1Id, Guid user2Id);

        Task<ChatMessage> SendMessageAsync(SendMessageDto dto);


    }
}
