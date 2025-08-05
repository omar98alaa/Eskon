using AutoMapper;
using Eskon.Domian.DTOs.Favourite;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.FavouriteMapping
{
    public partial class FavouriteProfileMapping : Profile
    {
        public FavouriteProfileMapping()
        {
            CreateMap<Favourite, FavouriteReadDTO>()
           .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title));

            CreateMap<FavouriteRequestDTO, Favourite>();
        }
    }
}
