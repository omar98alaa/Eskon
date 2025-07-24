using AutoMapper;

namespace Eskon.Core.Mapping.CityMapping
{
    public partial class CityMapping : Profile
    {
        public CityMapping()
        {
            UpdateCityMapping();
            ListCityMapping();
            GetCityMapping();


        }

    }
}
