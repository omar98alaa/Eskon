using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class PaymentService : IPaymentService
    {

        #region Fields
        private readonly IPaymentRepository _paymentRepository;
        #endregion

        #region Constructors
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        #endregion

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            await _paymentRepository.AddAsync(payment);
            return payment;
        }

        public async Task<List<Payment>> GetPaymentsPerCustomer(Guid customerId)
        {
            return await _paymentRepository.GetFilteredAsync(f => f.CustomerId == customerId);
        }

        public async Task SetPaymentAsSuccessful(Payment payment)
        {
            payment.State = "SUCCESS";
            await _paymentRepository.UpdateAsync(payment);
        }
    }
}
