using Eskon.Domian.Entities;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IEscrowTransactionRepository : IGenericRepositoryAsync<EscrowTransaction>
    {
        Task<EscrowTransaction?> GetByBookingIdAsync(Guid bookingId);
        Task<List<EscrowTransaction>> GetAllPendingReleasesAsync();
    }
}
