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
    public class GetCountryMapper : Profile
    {
        public GetCountryMapper() 
        {
            CreateMap<Country, CountryDTO>()
                 .ForMember(dest => dest.Cities,
                     opt => opt.MapFrom(src => src.Cities));


        }
    }
}
