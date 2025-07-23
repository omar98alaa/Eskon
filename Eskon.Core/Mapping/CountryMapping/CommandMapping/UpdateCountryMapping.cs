using AutoMapper;
using Eskon.Domian.DTOs.CityDTO;
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
        public void UpdateCountryMapping() {
            CreateMap<Country, CountryUpdateDTO>();
        }
    }
}
