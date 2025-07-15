using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Domian.Entities.Identity;

namespace Template.Domian.Models
{
   public class Transaction : BaseModel
    {
        [Required, Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal Fee { get; set; }

        [DefaultValue(false)]
        public bool IsRefund { get; set; }

        [Required]
        public DateOnly ConfirmedDate { get; set; }

        //
        //  Navigation Properties
        //

        //  Sender
        //[ForeignKey(nameof(Sender))]
        public Guid SenderId { get; set; }
        public virtual User Sender { get; set; }

        //  Receiver
        //[ForeignKey(nameof(Receiver))]
        public Guid ReceiverId { get; set; }
        public virtual User Receiver { get; set; }
    }
}
