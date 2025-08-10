using AutoMapper;

namespace Eskon.Core.Mapping.Chats
{
    public partial class ChatMessageMapping : Profile
    {
        public ChatMessageMapping()
        {
            ChatMessageProfile();
            SendMessageProfile();
            conversationMapping();
        }
    }
}
