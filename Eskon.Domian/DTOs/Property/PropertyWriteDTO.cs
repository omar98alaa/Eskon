using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.Models;

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
        [DefaultValue(false)]
        public bool IsSuspended { get; set; }

        //[DefaultValue(0.0)]
        public decimal AverageRating { get; set; }
        public string City { get; set; }

        public string Country { get; set; }

        public List<string> Images { get; set; }
    }
}
