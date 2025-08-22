using Askon.Infrastructure.Migrations;
using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class ChatMessageRepository : GenericRepositoryAsync<ChatMessage>, IChatMessageRepository
    {
        #region Fields
        private readonly DbSet<ChatMessage> _chatMessagesDbSet;
        private readonly DbSet<Chat> _chatDbSet;
        #endregion

        #region Constructors
        public ChatMessageRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _chatMessagesDbSet = myDbContext.Set<ChatMessage>();
            _chatDbSet= myDbContext.Set<Chat>();
        }
        #endregion

        public async Task<List<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId)
        {
            return await _chatMessagesDbSet
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task AddMessageAsync(ChatMessage message)
        {
            await _chatMessagesDbSet.AddAsync(message);
        }
        public async Task<ChatMessage> GetLastMessageAsync (Guid chatId, Guid userId)
        {
          return   await _chatMessagesDbSet
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
