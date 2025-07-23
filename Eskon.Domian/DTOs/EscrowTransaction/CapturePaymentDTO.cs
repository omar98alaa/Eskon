
namespace Eskon.Domian.DTOs.EscrowTransaction
{
    public class CapturePaymentDto
    {
        public Guid BookingId { get; set; }
        public string TransactionReference { get; set; }
    }
}
