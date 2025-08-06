using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.StripeDTOs
{
    public class CreateStripeCheckoutRequestDTO
    {
        [Required]
        [Url]
        public string SuccessUrl { get; set; }

        [Required]
        [Url]
        public string CancelUrl { get; set; }
    }
}
