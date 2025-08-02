using AutoMapper;
using Eskon.Domian.DTOs.CountryDTOs;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.CountryMapping
{
    partial class CountryMapper : Profile
    {
        public void AddCountryMapper()
        {
            CreateMap<AddCountryDTO, Country>()
          .ForMember(dest => dest.Cities, opt => opt.Ignore());
            CreateMap<Country, AddCountryDTO>();
        }
    }
}
