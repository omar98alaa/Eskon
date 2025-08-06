using Eskon.Domian.DTOs.CityDTOs;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.CountryDTOs
{
    public class CountryDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string Name { get; set; }
        public List<CityListDTO> Cities { get; set; }
    }
}
