using Eskon.Domian.Entities;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class EscrowTransactionRepository : GenericRepositoryAsync<EscrowTransaction>, IEscrowTransactionRepository
    {
        #region Fields
        private readonly DbSet<EscrowTransaction> _escrowTransactionRepository;
        #endregion

        #region Constructors
        public EscrowTransactionRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _escrowTransactionRepository = myDbContext.Set<EscrowTransaction>();
        }
        #endregion

        #region Methods
        public async Task<EscrowTransaction?> GetByBookingIdAsync(Guid bookingId)
        {
            return await _escrowTransactionRepository
                .FirstOrDefaultAsync(e => e.BookingId == bookingId);
        }

        public async Task<List<EscrowTransaction>> GetAllPendingReleasesAsync()
        {
            return await _escrowTransactionRepository
                .Where(e => e.IsPaymentCaptured && !e.IsReleasedToOwner && !e.IsRefunded)
                .ToListAsync();
        }
        #endregion
    }
}
