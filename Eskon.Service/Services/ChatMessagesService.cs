using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return  await _chatMessageRepository.AddAsync(chatMessage);
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(Guid chatId)
        {
           return await _chatMessageRepository.GetFilteredAsync(c=>c.ChatId==chatId);
        }

        public async Task<ChatMessage?> GetMessageByIdAsync(Guid messageId)
        {
           return await _chatMessageRepository.GetByIdAsync(messageId);
        }
    }

}
