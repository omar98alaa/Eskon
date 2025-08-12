using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Infrastructure.Repositories;
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
    }

}
