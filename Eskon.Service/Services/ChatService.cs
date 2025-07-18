using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Chat> AddChatAsync(Chat chat)
        {
            return await _chatRepository.AddAsync(chat);
        }

        public async Task<Chat?> GetChatByIdAsync(Guid chatId)
        {
          return await  _chatRepository.GetByIdAsync(chatId);
        }

        public async Task<List<Chat>> GetUserChatsAsync(Guid userId)
        {
          return await  _chatRepository.GetChatsForUserAsync(userId);
        }

        public async Task<bool> ChatExistsAsync(Guid user1Id, Guid user2Id)
        {
             return await _chatRepository.ChatExistsAsync(user1Id, user2Id);
        }
           
    }

}
