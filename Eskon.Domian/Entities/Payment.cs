using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eskon.Domian.Entities.Identity;

namespace Eskon.Domian.Models
{
    public class Payment : BaseModel
    {
        [Required, Range(0.0, double.MaxValue)]
        public decimal BookingAmount { get; set; }


        [Required, Range(0.0, double.MaxValue)]
        public decimal Fees { get; set; }


        [StringLength(20)]
        public string State { get; set; } = "CREATED";

        public bool IsRefund { get; set; } = false;

        public DateOnly? MaximumRefundDate { get; set; }

        public string? StripeChargeId { get; set; } 

        //
        //  Navigation Properties
        //

        //  User
        //[ForeignKey(nameof(User))]
        public Guid CustomerId { get; set; }
        public virtual User Customer { get; set; }

        [Required, ForeignKey(nameof(Booking))]
        public Guid BookingId { get; set; }
        public virtual Booking Booking { get; set; }

    }
}
