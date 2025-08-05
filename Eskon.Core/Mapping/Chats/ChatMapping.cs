using AutoMapper;

namespace Eskon.Core.Mapping.Chats
{
    partial class ChatMessageMapping : Profile
    {

        public ChatMessageMapping()
        {
            ChatMessageProfile();
            SendMessageProfile();
            conversationmapping();


        }
    }
}
