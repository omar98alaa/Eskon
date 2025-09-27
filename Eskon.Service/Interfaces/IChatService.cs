using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IChatService
    {
        Task<Chat> AddChatAsync(Chat chat);
        Task<Chat?> GetChatByIdAsync(Guid chatId);
        Task<List<Chat>> GetAllUserChatsAsync(User user);
        Task<bool> ChatExistsAsync(User user1, User user2);
        Task<Chat?> GetChatBetweenUsersAsync(User user1, User user2);
    }
}
