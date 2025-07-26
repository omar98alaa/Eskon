using AutoMapper;
using Eskon.Domian.DTOs.CityDTO;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.CityMapping
{
    partial class CityMapping : Profile
    {
        public void UpdateCityMapping()
        {
            CreateMap<City, CityUpdateDTO>();
        }
    }
}
