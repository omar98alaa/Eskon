using Eskon.Domian.Models;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.Stripe
{
    public class CreateStripeCheckoutRequestDTO
    {
        [Required]
        public Booking Booking { get; set; }

        [Required]
        [Url]
        public string SuccessUrl { get; set; }

        [Required]
        [Url]
        public string CancelUrl { get; set; }
    }
}
