using AutoMapper;
using Eskon.Domian.DTOs.CityDTOs;
using Eskon.Domian.DTOs.CityDTOs;
using Eskon.Domian.DTOs.CountryDTOs;
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
        public void GetCountryMapper()
        {

            CreateMap<Country, CountryDTO>();
        }

    }
}
