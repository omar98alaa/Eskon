using Eskon.Domian.Entities;
using Eskon.Domian.Entities.Identity;



namespace Eskon.Domian.DTOs.Booking
{
    public class BookingReadDTO
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public Guid PropertyId { get; set; }
        public Guid OwnerID { get; set; }
        public string? OwnerName { get; set; }
    }
}
