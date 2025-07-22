
namespace Eskon.Domain.DTOs.Transaction
{
    public class TransactionInputDTO
    {
        public decimal Amount { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
