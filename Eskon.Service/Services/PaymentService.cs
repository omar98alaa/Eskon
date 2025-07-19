using System.ComponentModel.DataAnnotations;
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

        public async Task<List<Payment>> GetPaymentsPerUser(Guid userId)
        {
            return await _paymentRepository.GetFilteredAsync(f => f.UserId == userId);
        }

        public async Task SetPaymentAsSuccessful(Payment payment)
        {
            payment.IsSuccessful = true;
            await _paymentRepository.UpdateAsync(payment);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _paymentRepository.SaveChangesAsync();   
        }
    }
}
