using Eskon.Domian.Entities;

namespace Eskon.Service.Interfaces
{
    public interface IEscrowTransactionService
    {
        #region Create
        public Task<EscrowTransaction> AddEscrowTransactionAsync(EscrowTransaction escrowTransaction);
        #endregion

        #region Read
        public Task<EscrowTransaction?> GetByBookingIdAsync(Guid bookingId);
        public Task<List<EscrowTransaction>> GetAllPendingReleasesAsync();
        #endregion

        #region Business Actions (return success/failure or result objects)
        public Task<bool> MarkPaymentCapturedAsync(Guid bookingId, string transactionReference);
        public Task<bool> MarkReleasedToOwnerAsync(Guid bookingId);
        public Task<bool> MarkRefundedToCustomerAsync(Guid bookingId);
        #endregion
    }

}
