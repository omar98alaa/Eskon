using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepositoryAsync<Transaction>, ITransactionRepository
    {
        private readonly MyDbContext _context;

        public TransactionRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetAllTransactionsForUserAsync(Guid userId)
        {
            return await _context.Transactions
                .Where(t => t.SenderId == userId || t.ReceiverId == userId)
                .Include(s => s.Sender)
                .Include(s => s.Receiver)
                .ToListAsync();
        }
        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .Include(s => s.Sender)
                .Include(s => s.Receiver).ToListAsync();
        }
        public async Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Transactions
                .Where(t => t.CreatedAt >= startDate && t.CreatedAt <= endDate)
                .ToListAsync();
        }

    }
}
