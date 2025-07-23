using AutoMapper;
using Eskon.Domian.DTOs.City;
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
        public void ListCityMapping()
        {
            CreateMap<City, CityListDTO>();
        }




    }
}
