using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<Transaction>> GetAllForUserAsync(Guid userId)
        {
            return await _transactionRepository.GetAllTransactionsForUserAsync(userId);
        }

        public async Task<List<Transaction>> GetAllForOwnerAsync(Guid ownerId)
        {
            return await _transactionRepository.GetAllTransactionsForOwnerAsync(ownerId);
        }



        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChangesAsync();
            return transaction;
        }
    }
}
