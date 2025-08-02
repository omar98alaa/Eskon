using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.BookingDTOs
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
