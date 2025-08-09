using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.Chats
{
    public partial class ChatMessageMapping
    {
        public void ChatMessageProfile()
        {
            CreateMap<ChatMessage, ChatMessageDto>()
            .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.SenderId == src.Chat.User1Id ? src.Chat.User2Id : src.Chat.User1Id))
            .ReverseMap();
        }
    }
}