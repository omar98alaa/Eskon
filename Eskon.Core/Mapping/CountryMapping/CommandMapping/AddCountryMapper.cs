using AutoMapper;
using Eskon.Domian.DTOs.Country;
using Eskon.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
