
using System.ComponentModel.DataAnnotations;

namespace Eskon.Domain.DTOs.Transaction
{
    public class TransactionInputDTO
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public Guid RecieverId { get; set; }

    }
}
