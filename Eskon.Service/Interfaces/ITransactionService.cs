using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllForUserAsync(Guid userId);
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime StartDate, DateTime EndDate);
        Task<Transaction> CreateTransactionAsync(Transaction transaction);
    }
}
