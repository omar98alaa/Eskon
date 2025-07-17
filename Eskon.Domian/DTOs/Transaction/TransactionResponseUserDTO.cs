using Eskon.Domian.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Domain.DTOs.Transaction
{
    public class TransactionResponseUserDTO
    {
        public Guid Id { get; set; }
        public decimal TotalCost { get; set; }
        public DateOnly ConfirmedDate { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public Guid ReceiverId { get; set; }
        public string ReceiverName { get; set; }
    }
}
