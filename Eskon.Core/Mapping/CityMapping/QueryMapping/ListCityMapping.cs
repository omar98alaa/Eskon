using AutoMapper;
using Eskon.Domian.DTOs.City;
using Eskon.Domian.Models;

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
