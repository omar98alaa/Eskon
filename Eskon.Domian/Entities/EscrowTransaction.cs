
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;

namespace Eskon.Domian.Entities
{
    public class EscrowTransaction : BaseModel
    {
        public Guid BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        // Guest who paid
        public Guid CustomerId { get; set; }
        public virtual User Customer { get; set; }

        // Host who will receive the money
        public Guid OwnerId { get; set; }
        public virtual User Owner { get; set; }

        // Financial details
        public decimal TotalAmount { get; set; }
        public decimal EskonFee { get; set; }
        public decimal PaymentGatewayFee { get; set; }
        public decimal OwnerReceivable => TotalAmount - EskonFee - PaymentGatewayFee;


        // Payment state
        public bool IsPaymentCaptured { get; set; } = false;
        public DateTime PaymentCapturedAt { get; set; }

        public bool IsReleasedToOwner { get; set; } = false;
        public DateTime? ReleasedAt { get; set; }

        public bool IsRefunded { get; set; } = false;
        public DateTime? RefundedAt { get; set; }

        public string PaymentMethod { get; set; } // "CreditCard", "PayPal", etc.
        public string TransactionReference { get; set; } // from Stripe/Adyen

    }

}
