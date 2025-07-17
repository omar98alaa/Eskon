using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Domian.Models
{
    [Index(nameof(UserId), nameof(PropertyId), nameof(StartDate), nameof(EndDate), IsUnique = true)]
    public class Booking : BaseModel
    {
        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [DefaultValue(true)]
        public bool IsPending { get; set; }

        [DefaultValue(false)]
        public bool IsAccepted { get; set; }

        [DefaultValue(false)]
        public bool IsPayed { get; set; }


        //
        //  Navigation Properties
        //

        //  User
        //[ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        //  Property
        //[ForeignKey(nameof(Property))]
        public Guid PropertyId { get; set; }
        public virtual Property Property { get; set; }
    }
}
