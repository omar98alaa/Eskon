
namespace Eskon.Domian.DTOs.Favourite
{
    public class FavouriteReadDTO
    {
        public Guid Id { get; set; }
        public Guid propertyId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public int MaxGuests { get; set; }
        public decimal PricePerNight { get; set; }
        public string ThumbnailUrl { get; set; }
        public decimal AverageRating { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}