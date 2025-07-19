using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.Entities;
using Eskon.Domian.Models;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllForUserAsync(Guid userId);
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime StartDate, DateTime EndDate);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
        Task<int> SaveChangesAsync();
        


    }
}
