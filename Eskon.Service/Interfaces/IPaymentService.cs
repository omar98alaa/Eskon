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

        #endregion

        #region Update
        public Task SetPaymentAsSuccessful(Payment payment);
        
        #endregion

    }
}
