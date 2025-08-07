
namespace Eskon.Domian.DTOs.Favourite
{
    public class FavouriteReadDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int MaxGuests { get; set; }
        public decimal PricePerNight { get; set; }
        public string ThumbnailUrl { get; set; }
        public string? RejectionMessage { get; set; }
        public decimal AverageRating { get; set; }
        public string City { get; set; }

        public string Country { get; set; }

    }
}