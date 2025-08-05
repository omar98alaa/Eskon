using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class ChatRepository : GenericRepositoryAsync<Chat>, IChatRepository
    {
        #region Fields
        private readonly DbSet<Chat> _chatDbSet;
        #endregion

        #region Constructors
        public ChatRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _chatDbSet = myDbContext.Set<Chat>();

        }
        #endregion

        public async Task<List<Chat>> GetChatsForUserAsync(Guid userId)
        {
            return await _chatDbSet
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .ToListAsync();
        }
        public async Task<bool> ChatExistsAsync(Guid user1Id, Guid user2Id)
        {
            return await _chatDbSet.AnyAsync(c =>
                (c.User1Id == user1Id && c.User2Id == user2Id) ||
                (c.User1Id == user2Id && c.User2Id == user1Id));
        }
        public async Task<ChatMessage> SendMessageAsync(SendMessageDto dto)
        {
            var senderId = dto.SenderId;
            var receiverId = dto.ReceiverId;

            Guid chatId;

            if (dto.ChatId != Guid.Empty)
            {
                chatId = dto.ChatId;
            }
            else
            {
                var existingChat = await _chatDbSet.FirstOrDefaultAsync(c =>
                    (c.User1Id == senderId && c.User2Id == receiverId) ||
                    (c.User1Id == receiverId && c.User2Id == senderId));

                if (existingChat != null)
                {
                    chatId = existingChat.Id;
                }
                else
                {
                    var newChat = new Chat
                    {
                        Id = Guid.NewGuid(),
                        User1Id = senderId,
                        User2Id = receiverId,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _chatDbSet.AddAsync(newChat);
                    chatId = newChat.Id;
                }
            }

            var message = new ChatMessage
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            return message;
        }




    }
}
