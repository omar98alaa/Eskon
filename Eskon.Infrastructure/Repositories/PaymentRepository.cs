using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepositoryAsync<Payment>, IPaymentRepository
    {
        #region Fields
        private readonly DbSet<Payment> _paymentDbSet;
        #endregion

        #region Constructors
        public PaymentRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _paymentDbSet = myDbContext.Set<Payment>();
        }
        #endregion

        #region Methods
        public async Task<Payment?> GetPaymentByChargedId(string chargedId)
        {
            return await _paymentDbSet.SingleOrDefaultAsync(p => p.StripeChargeId == chargedId);
        }

        public async Task<Payment?> GetPaymentByBookingIdAsync(Guid bookingId)
        {
            return await _paymentDbSet.SingleOrDefaultAsync(p => p.BookingId == bookingId);
        }
        public async Task<Dictionary<string, decimal>> GetRevenueByMonthAsync()
        {
            return await _paymentDbSet
                .GroupBy(p => new { p.CreatedAt.Year, p.CreatedAt.Month })
                .Select(g => new
                {
                    Month = g.Key.Year + "-" + g.Key.Month,
                    Revenue = g.Sum(p => p.Fees)
                })
                .ToDictionaryAsync(x => x.Month, x => x.Revenue);
        }
        #endregion
    }
}
