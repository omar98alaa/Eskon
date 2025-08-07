using AutoMapper;
using Eskon.Domian.DTOs.Favourite;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.FavouriteMapping
{
    public partial class FavouriteMapping : Profile
    {
        public FavouriteMapping()
        {
            CreateMap<Favourite, FavouriteReadDTO>()
            .ForMember(dest => dest.Id,              opt => opt.MapFrom(src => src.PropertyId))
            .ForMember(dest => dest.Title,           opt => opt.MapFrom(src => src.Property.Title))
            .ForMember(dest => dest.MaxGuests,       opt => opt.MapFrom(src => src.Property.MaxGuests))
            .ForMember(dest => dest.PricePerNight,   opt => opt.MapFrom(src => src.Property.PricePerNight))
            .ForMember(dest => dest.RejectionMessage,opt => opt.MapFrom(src => src.Property.RejectionMessage))
            .ForMember(dest => dest.ThumbnailUrl,    opt => opt.MapFrom(src => src.Property.ThumbnailUrl))
            .ForMember(dest => dest.City,            opt => opt.MapFrom(src => src.Property.City.Name))
            .ForMember(dest => dest.Country,         opt => opt.MapFrom(src => src.Property.City.Country.Name));
        }
    }
}
