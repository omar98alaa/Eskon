using Eskon.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Service.Interfaces
{
    interface IChatService
    {
        Task<Chat> AddChatAsync(Chat chat);
        Task<Chat?> GetChatByIdAsync(Guid chatId);
        Task<List<Chat>> GetUserChatsAsync(Guid userId);
        Task<bool> ChatExistsAsync(Guid user1Id, Guid user2Id);
    }
}
