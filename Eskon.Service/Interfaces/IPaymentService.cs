using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IPaymentService
    {
        #region Create
        public Task<Payment> AddPaymentAsync(Payment payment);
        #endregion

        #region Read
        public Task<List<Payment>> GetPaymentsPerCustomer(Guid customerId);

        public Task<Payment?> GetPaymentByChargedId(string chargedId);

        public Task<Payment?> GetPaymentByBookingIdAsync(Guid bookingId);

        #endregion

        #region Update
        public Task SetPaymentAsSuccess(Payment payment);

        public Task SetPaymentAsFailed(Payment payment);

        public Task SetPaymentAsRefunded(Payment payment);

        #endregion

    }
}
