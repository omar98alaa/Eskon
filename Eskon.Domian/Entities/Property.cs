using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Domian.Models
{
    [Index(nameof(Title), nameof(OwnerId), nameof(Longitude), nameof(Latitude), IsUnique = true)]
    public class Property : BaseModel
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
        public int NumOfBathrooms{ get; set; }

        [Range(0.0, (double)decimal.MaxValue)]
        public decimal PricePerNight { get; set; } = 1.0m;

        [Required]
        public string ThumbnailUrl { get; set; }

        [Required, Range(0.0, double.MaxValue)]
        public decimal Area { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [DefaultValue(true)]
        public bool IsPending { get; set; }

        [DefaultValue(false)]
        public bool IsSuspended { get; set; }

        [DefaultValue(false)]
        public bool IsAccepted { get; set; }

        [StringLength(200)]
        public string RejectionMessage { get; set; }

        public decimal AverageRating { get; set; } = 0.0m;

        //
        //  Navigation Properties
        //

        public Guid OwnerId { get; set; }
        public virtual User Owner { get; set; }

        //  Property type
        public Guid PropertyTypeId { get; set; }
        public virtual PropertyType PropertyType { get; set; }


        //  City
        public Guid CityId { get; set; }
        public virtual City City { get; set; }


        //  AssignedAdmin
        public Guid AssignedAdminId { get; set; }
        public virtual User AssignedAdmin { get; set; }

        //  Bookings
        public virtual ICollection<Booking> Bookings { get; set; }

        //  Favourites
        public virtual ICollection<Favourite> Favourites { get; set; }

        //  Images
        public virtual ICollection<Image> Images { get; set; }

        //  Reviews
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
