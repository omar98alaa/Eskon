using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;
using System.Runtime.Intrinsics.X86;

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
            return await _chatRepository.GetByIdAsync(chatId);
        }

        public async Task<List<Chat>> GetAllUserChatsAsync(User user)
        {
            return await _chatRepository.GetChatsForUserAsync(user.Id);
        }

        public async Task<bool> ChatExistsAsync(User user1, User user2)
        {
            return await _chatRepository.ChatExistsAsync(user1.Id, user2.Id);
        }

        public async Task<ChatMessage> SendMessageAsync(SendMessageDto dto)
        {
            return await _chatRepository.SendMessageAsync(dto);
        }

    }

}
