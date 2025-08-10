using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.Chats
{
    public partial class ChatMessageMapping
    {
        public void conversationMapping()
        {
            CreateMap<Chat, ConversationDto>()
            .ForMember(dest => dest.User2Name, opt => opt.MapFrom(src => src.User2.FirstName + ' ' + src.User2.LastName))
            .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.ChatMessages.Last().Content))
            .ForMember(dest => dest.LastMessageTime, opt => opt.MapFrom(src => src.ChatMessages.Last().CreatedAt))
            .ForMember(dest => dest.UnreadCount, opt => opt.MapFrom(src => src.ChatMessages.Count(m => !m.IsRead)))
            .ReverseMap();
        }
    }
}
