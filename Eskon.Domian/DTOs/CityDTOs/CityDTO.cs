using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.CityDTOs
{
    public class CityDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Country Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        public string CountryName { get; set; }

    }

}
