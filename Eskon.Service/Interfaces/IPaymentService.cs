using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IPaymentService
    {
        #region Create
        public Task<Payment> AddPaymentAsync(Payment payment);
        #endregion

        #region Read
        public Task<List<Payment>> GetPaymentsPerUser(Guid userId);

        public Payment GetPaymentByChargedId(string chargedId);

        #endregion

        #region Update
        public Task SetPaymentAsSuccess(Payment payment);

        public Task SetPaymentAsFailed(Payment payment);

        #endregion

    }
}
