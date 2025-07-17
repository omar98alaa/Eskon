using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface ITransactionRepository : IGenericRepositoryAsync<Transaction>
    {
        Task<List<Transaction>> GetAllTransactionsForUserAsync(Guid userId);
        Task<List<Transaction>> GetAllTransactionsForOwnerAsync(Guid ownerId);
    }
}
