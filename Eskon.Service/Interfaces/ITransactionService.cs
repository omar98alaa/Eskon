using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.Entities;
using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllForUserAsync(Guid userId);
        Task<List<Transaction>> GetAllForOwnerAsync(Guid ownerId);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
    }
}
