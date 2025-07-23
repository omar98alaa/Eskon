using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;

namespace Eskon.Domian.Entities
{
    public class PaymentMethod : BaseModel
    {
        public Guid OwnerId { get; set; }

        public string Provider { get; set; } // e.g., Stripe, PayPal
        public string ProviderAccountId { get; set; } // e.g., Stripe Connect ID

        public string BankName { get; set; }
        public string AccountHolderName { get; set; }
        public bool IsVerified { get; set; } = false;
        public bool IsDefault { get; set; } = false;
        public DateTime? VerifiedAt { get; set; }

        public virtual User Owner { get; set; }
    }

}
