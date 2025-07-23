
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domian.DTOs.EscrowTransaction
{
    public class AddEscrowTransactionDTO
    {
        [Required]
        public Guid BookingId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public decimal EskonFee { get; set; }

        [Required]
        public decimal PaymentGatewayFee { get; set; }

        [Required]
        public string TransactionReference { get; set; } = string.Empty;

        [Required] 
        public string PaymentMethod { get; set; } // "CreditCard", "PayPal", etc.
    }
}
