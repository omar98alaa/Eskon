using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.Chats
{
    public partial class ChatMessageMapping
    {
        public void conversationmapping()
        {
            CreateMap<Chat, ConversationDto>()
            .ForMember(dest => dest.User2Id, opt => opt.MapFrom((src, dest, _, context) =>
                src.User1Id == (Guid)context.Items["CurrentUserId"] ? src.User2Id : src.User1Id))
            .ForMember(dest => dest.User2Name, opt => opt.MapFrom((src, dest, _, context) =>
                src.User2Id == (Guid)context.Items["CurrentUserId"] ? src.User2.UserName : src.User1.UserName))
            .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.ChatMessages.Last().Content))
            .ForMember(dest => dest.LastMessageTime, opt => opt.MapFrom(src => src.ChatMessages.Last().CreatedAt))
            .ForMember(dest => dest.UnreadCount, opt => opt.MapFrom(src => src.ChatMessages.Count(m => !m.IsRead)))
            .ReverseMap();
        }
    }
}
