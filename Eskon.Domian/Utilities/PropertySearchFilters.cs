namespace Eskon.Domian.Utilities
{
    public class PropertySearchFilters
    {
        public decimal? minPricePerNight { get; set; }
        public decimal? maxPricePerNight { get; set; }
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
        public int? Guests { get; set; }
        public string? SortBy { get; set; }
        public bool Asc { get; set; }
    }
}
