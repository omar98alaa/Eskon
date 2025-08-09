using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.Chats
{
    public partial class ChatMessageMapping
    {
        public void SendMessageProfile()
        {
            CreateMap<SendMessageDto, ChatMessage>();
        }
    }
}
