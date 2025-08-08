namespace Eskon.Domian.DTOs.BookingDTOs
{
    public class BookingReadDTO
    {
        public Guid Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
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
