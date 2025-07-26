using AutoMapper;
using Eskon.Domian.DTOs.City;
using Eskon.Domian.Models;


namespace Eskon.Core.Mapping.CityMapping
{
    partial class CityMapping : Profile
    {
        public void CityReadDTOMapping()
        {
            CreateMap<City, CityReadDTO>().AfterMap((src, dest) =>
            {
                dest.CityName = src.Name;
                dest.CountryName = src.Country.Name;
            });
        }

    }
}
