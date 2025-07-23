using Eskon.Domian.Entities;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class EscrowTransactionService : IEscrowTransactionService
    {
        #region Fields
        private readonly IEscrowTransactionRepository _escrowtransactionRepository;
        #endregion

        #region Constructors
        public EscrowTransactionService(IEscrowTransactionRepository escrowtransactionRepository)
        {
            _escrowtransactionRepository = escrowtransactionRepository;
        }
        #endregion

        #region Methods

        #region Create
        public async Task<EscrowTransaction> AddEscrowTransactionAsync(EscrowTransaction escrowTransaction)
        {
            return await _escrowtransactionRepository.AddAsync(escrowTransaction);
        }
        #endregion

        #region Read
        public async Task<EscrowTransaction?> GetByBookingIdAsync(Guid bookingId)
        {
            return await _escrowtransactionRepository.GetByBookingIdAsync(bookingId);
        }

        public async Task<List<EscrowTransaction>> GetAllPendingReleasesAsync()
        {
            return await _escrowtransactionRepository.GetAllPendingReleasesAsync();
        }
        #endregion

        #region Business Actions (return success/failure or result objects)
        public async Task<bool> MarkPaymentCapturedAsync(Guid bookingId, string transactionReference)
        {
            var escrow = await _escrowtransactionRepository.GetByBookingIdAsync(bookingId);
            if (escrow == null || escrow.IsPaymentCaptured) return false;

            escrow.IsPaymentCaptured = true;
            escrow.PaymentCapturedAt = DateTime.UtcNow;
            escrow.TransactionReference = transactionReference;
            return true;
        }

        public async Task<bool> MarkReleasedToOwnerAsync(Guid bookingId)
        {
            var escrow = await _escrowtransactionRepository.GetByBookingIdAsync(bookingId);
            if (escrow == null || escrow.IsReleasedToOwner || escrow.IsRefunded) return false;

            escrow.IsReleasedToOwner = true;
            escrow.ReleasedAt = DateTime.UtcNow;
            return true;
        }

        public async Task<bool> MarkRefundedToCustomerAsync(Guid bookingId)
        {
            var escrow = await _escrowtransactionRepository.GetByBookingIdAsync(bookingId);
            if (escrow == null || escrow.IsReleasedToOwner || escrow.IsRefunded) return false;

            escrow.IsRefunded = true;
            escrow.RefundedAt = DateTime.UtcNow;
            return true;
        }

        #endregion

        #endregion
    }


}
