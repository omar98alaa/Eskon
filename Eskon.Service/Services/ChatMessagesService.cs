using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class ChatMessageService : IChatMessagesService
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatMessageService(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<ChatMessage> AddMessageAsync(ChatMessage chatMessage)
        {
            return await _chatMessageRepository.AddAsync(chatMessage);
        }

        public async Task<List<ChatMessage>> GetMessagesByChatIdAsync(Guid chatId)
        {
            return await _chatMessageRepository.GetMessagesByChatIdAsync(chatId);
        }


        public async Task UpdateMessageAsync(ChatMessage chatMessage)
        {
            await _chatMessageRepository.UpdateAsync(chatMessage);
        }
        public async Task DeleteMessageAsync(ChatMessage chatMessage)
        {
            await _chatMessageRepository.SoftDeleteAsync(chatMessage);
        }
        public async Task MarkMessagesAsRead(Chat chat, Guid userId)
        {
            var messagesToMark = chat.ChatMessages
                .Where(m => m.SenderId != userId && !m.IsRead)
                .ToList();

            foreach (var message in messagesToMark)
            {
                message.IsRead = true;
                message.ReadAt = DateTime.UtcNow;
                await _chatMessageRepository.UpdateAsync(message);
            }

        }

        public async Task<ChatMessage?> GetlastMessagesAsync(Guid chat, Guid userId)
        {
        return await _chatMessageRepository.GetLastMessageAsync(chat, userId);
        }

    }

}
