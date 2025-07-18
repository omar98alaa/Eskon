using Eskon.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Service.Interfaces
{
    interface IChatMessagesService
    {
        Task<ChatMessage> AddMessageAsync(ChatMessage chatMessage);
        Task<List<ChatMessage>> GetMessagesAsync(Guid chatId);
        Task<ChatMessage?> GetMessageByIdAsync(Guid messageId);
    }
}
