using AutoMapper;
using Eskon.Domian.DTOs.Country;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.CountryMapping
{
    partial class CountryMapper : Profile
    {
        public void CountryReadDTOMapping()
        {
            CreateMap<Country, CountryReadDTO>();
        }

    }
}
