using AutoMapper;
using Eskon.Domian.DTOs.City;
using Eskon.Domian.DTOs.CityDTO;
using Eskon.Domian.DTOs.Country_City;
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
