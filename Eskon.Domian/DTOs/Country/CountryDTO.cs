using Eskon.Domian.DTOs.City;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.Country_City
{
    public class CountryDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string Name { get; set; }
        public List<CityListDTO> Cities { get; set; }
    }
}
