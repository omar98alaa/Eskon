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
                .Where(t => t.SenderId == userId)
                .Include(s => s.Sender)
                .Include(s => s.Receiver)
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionsForOwnerAsync(Guid ownerId)
        {
            return await _context.Transactions
                .Where(t => t.ReceiverId == ownerId)
                .Include(s => s.Sender)
                .Include(s => s.Receiver)
                .ToListAsync();
        }


    }
}
