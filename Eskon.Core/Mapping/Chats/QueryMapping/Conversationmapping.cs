using Eskon.Domian.DTOs.Chat;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.Chats
{
    public partial class ChatMessageMapping
    {
        public void conversationmapping()
        {
            CreateMap<ChatMessage, ConversationDto>()
    .ForMember(dest => dest.User2Id, opt => opt.MapFrom((src, dest, _, context) =>
        src.SenderId == (Guid)context.Items["CurrentUserId"] ? src.Receiver.Id : src.Sender.Id))
    .ForMember(dest => dest.User2Name, opt => opt.MapFrom((src, dest, _, context) =>
        src.SenderId == (Guid)context.Items["CurrentUserId"] ? src.Receiver.UserName : src.Sender.UserName))
    .ForMember(dest => dest.LastMessage, opt => opt.MapFrom(src => src.Content))
    .ForMember(dest => dest.LastMessageTime, opt => opt.MapFrom(src => src.CreatedAt))
    .ForMember(dest => dest.UnreadCount, opt => opt.Ignore());

        }
    }
}
