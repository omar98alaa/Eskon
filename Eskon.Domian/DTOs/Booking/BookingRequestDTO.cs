using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.Booking
{
    public class BookingRequestDTO
    {
        [Required]
        public Guid PropertyId { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }
    }
}
