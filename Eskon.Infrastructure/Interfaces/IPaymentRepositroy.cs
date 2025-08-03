using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IPaymentRepository : IGenericRepositoryAsync<Payment>
    {
        public Payment GetPaymentByChargedId(string chargedId);
        public Task<Payment> GetPaymentByBookingIdAsync(Guid bookingId);
    }
}
