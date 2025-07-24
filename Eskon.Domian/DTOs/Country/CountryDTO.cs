using Eskon.Domian.DTOs.City;

namespace Eskon.Domian.DTOs.Country_City
{
    public class CountryDTO
    {
        public string Name { get; set; }
        public List<CityListDTO> Cities { get; set; }
    }
}
