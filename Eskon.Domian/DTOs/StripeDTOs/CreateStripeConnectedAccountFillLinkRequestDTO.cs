using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.StripeDTOs
{
    public class CreateStripeConnectedAccountFillLinkRequestDTO
    {
        [Required]
        public string stripeAccountId {  get; set; } 

        [Required]
        [Url]
        public string refreshUrl { get; set; }

        [Required]
        [Url]
        public string returnUrl { get; set; }
    }
}
