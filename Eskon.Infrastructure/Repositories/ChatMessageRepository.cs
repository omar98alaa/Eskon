using Eskon.Domian.DTOs.Chat;
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

        public async Task<List<ConversationDto>> GetConversationsForUserAsync(Guid userId)
        {
            var chats = await _chatDbSet
           .Include(c => c.User1)
           .Include(c => c.User2)
           .ToListAsync();


            var messages = await _chatMessagesDbSet
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();

            var result = chats
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .Select(chat =>
                {
                    var lastMessage = messages
                        .Where(m => m.ChatId == chat.Id)
                        .OrderByDescending(m => m.CreatedAt)
                        .FirstOrDefault();

                    var unreadCount = messages
                        .Count(m => m.ChatId == chat.Id && m.ReceiverId == userId && !m.IsRead);

                    var otherUser = chat.User1Id == userId ? chat.User2 : chat.User1;

                    return new ConversationDto
                    {
                        ChatId = chat.Id,
                        User2Id = otherUser.Id.ToString(),
                        User2Name = otherUser.UserName,
                        LastMessage = lastMessage?.Content,
                        LastMessageTime = lastMessage?.CreatedAt,
                        UnreadCount = unreadCount
                    };
                })
                .OrderByDescending(c => c.LastMessageTime)
                .ToList();

            return result;
        }

        public async Task MarkMessagesAsReadAsync(Guid chatId, Guid userId)
        {
            var messages = await _chatMessagesDbSet
                .Where(m =>
                    m.ChatId == chatId &&
                    m.ReceiverId == userId &&
                    !m.IsRead)
                .ToListAsync();

            foreach (var message in messages)
            {
                message.IsRead = true;
                message.ReadAt = DateTime.UtcNow; // اختياري لو بتسجلي وقت القراءة
                _chatMessagesDbSet.Update(message);
            }

          
        }


    }
}
