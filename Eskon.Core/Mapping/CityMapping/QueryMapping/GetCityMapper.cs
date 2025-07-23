using AutoMapper;
using Eskon.Domian.DTOs.City;
using Eskon.Domian.DTOs.CityDTO;
using Eskon.Domian.Models;


namespace Eskon.Core.Mapping.CityMapping
{
    partial class CityMapping : Profile
    {
        public void GetCityMapping()
        {
            CreateMap<City, CityDTO>()
                .ForMember(dest => dest.CountryName,
                opt => opt.MapFrom(src => src.Country != null ? src.Country.Name : null));

        }

    }
}
