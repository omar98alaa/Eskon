using AutoMapper;
using Eskon.Domian.DTOs.CityDTO;
using Eskon.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Mapping.CityMapping
{
    partial class CityMapping : Profile
    {
        public void UpdateCityMapping() {
            CreateMap<City, CityUpdateDTO>();
        }
    }
}
