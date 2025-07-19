using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface ITransactionRepository : IGenericRepositoryAsync<Transaction>
    {
        Task<List<Transaction>> GetAllTransactionsForUserAsync(Guid userId);
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
