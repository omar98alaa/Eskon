using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.Property
{
    public class PropertyWriteDTO
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [DefaultValue(1)]
        public int MaxGuests { get; set; }

        [DefaultValue(1), Range(0, int.MaxValue)]
        public int NumOfBedrooms { get; set; }

        [DefaultValue(1), Range(0, int.MaxValue)]
        public int NumOfBeds { get; set; }

        [DefaultValue(1), Range(0, int.MaxValue)]
        public int NumOfBathrooms { get; set; }

        //[DefaultValue(1.0), Range(0.0, double.MaxValue)]
        public decimal PricePerNight { get; set; }

        [Required]
        public string ThumbnailUrl { get; set; }

        //[Required, Range(0.0, double.MaxValue)]
        public decimal Area { get; set; }
        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public Guid CityId { get; set; }

        [Required]
        public List<string> ImageUrls { get; set; }

        [Required]
        public Guid PropertyTypeId {  get; set; }
    }
}
