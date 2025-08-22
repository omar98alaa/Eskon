namespace Eskon.Domian.DTOs.ReviewDTOs
{
    public class ReviewReadDTO
    {
        public Guid Id { get; set; }
        public decimal Rating { get; set; }
        public string Content { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Guid PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
