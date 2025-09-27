using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.Chats
{
    public partial class ChatMessageMapping
    {
        public void conversationMapping()
        {
            CreateMap<Chat, ConversationDto>()
            .ForMember(dest => dest.User1Name, opt => opt.MapFrom(src => src.User1.FirstName + ' ' + src.User1.LastName))
            .ForMember(dest => dest.User2Name, opt => opt.MapFrom(src => src.User2.FirstName + ' ' + src.User2.LastName))
            .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src =>
             (src.ChatMessages != null && src.ChatMessages.Any())
            ? src.ChatMessages.OrderByDescending(m => m.CreatedAt).First().Content
            : null))
            .ForMember(dest => dest.LastMessageTime, opt => opt.MapFrom(src =>
             (src.ChatMessages != null && src.ChatMessages.Any())
            ? src.ChatMessages.OrderByDescending(m => m.CreatedAt).First().CreatedAt
            : (DateTime?)null))
           .ForMember(dest => dest.UnreadCount, opt => opt.MapFrom((src, dest, destMember, context) =>
    (src.ChatMessages != null)
        ? src.ChatMessages.Count(m => !m.IsRead && m.SenderId != (Guid)context.Items["CurrentUserId"])
        : 0
))

            .ReverseMap();
        }
    }
}
