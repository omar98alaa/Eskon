using Eskon.Domian.Entities;
using Eskon.Domian.Entities.Identity;
using System.ComponentModel;



namespace Eskon.Domian.DTOs.BookingDTOs
{
    public class BookingReadDTO
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateOnly CreatedAt { get; set; }
        public bool IsPending { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsPayed { get; set; }
        public int Guests { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid PropertyId { get; set; }

    }
}
