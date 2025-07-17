using AutoMapper;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Mapping.Country_CityMapping
{
    public class GetCityMapper : Profile
    {
        public GetCityMapper()
        {
            CreateMap<City, CityDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CountryName,
                opt => opt.MapFrom(src => src.Country != null ? src.Country.Name : null));
            CreateMap<City, CityReadDTO>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CountryName,
                opt => opt.MapFrom(src => src.Country != null ? src.Country.Name : null));

        }

    }
}
