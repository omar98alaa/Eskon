using AutoMapper;

namespace Eskon.Core.Mapping.CountryMapping
{
    partial class CountryMapper : Profile
    {
        public CountryMapper()
        {
            UpdateCountryMapping();
            GetCountryMapper();
            AddCountryMapper();
            CountryReadDTOMapping();
        }
    }
}
