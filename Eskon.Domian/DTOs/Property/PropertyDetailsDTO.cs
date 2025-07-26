namespace Eskon.Domian.DTOs.Property
{
    public class PropertyDetailsDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int MaxGuests { get; set; }

        public int NumOfBedrooms { get; set; }

        public int NumOfBeds { get; set; }

        public int NumOfBathrooms { get; set; }

        public decimal PricePerNight { get; set; }

        public string ThumbnailUrl { get; set; }

        public decimal Area { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public bool IsSuspended { get; set; }

        public decimal AverageRating { get; set; }

        public Guid OwnerId { get; set; }

        #region Requires Mapping
        //  Owner name
        public string OwnerName { get; set; }

        //  Property type
        public string PropertyType { get; set; }

        //  City
        public string City { get; set; }

        public string Country { get; set; }

        //  Images
        public List<string> ImageURLs { get; set; }
        #endregion
    }
}
