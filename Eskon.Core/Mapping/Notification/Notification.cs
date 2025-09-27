using AutoMapper;
using Eskon.Domian.DTOs.Notification;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping
{
    public class NotificationMapping  : Profile
    {
        public NotificationMapping ()
        {
            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.NotificationTypeName, opt => opt.MapFrom(src => src.NotificationType != null ? src.NotificationType.Name : null))
                .ForMember(dest => dest.RedirectionName, opt => opt.Ignore()); 
        }
    }
} 