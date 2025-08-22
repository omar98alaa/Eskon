using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IPropertyRepository : IGenericRepositoryAsync<Property>
    {
        public Task<int> GetAllPendingPropertiesCountPerAdminAsync(Guid assignedAdmin);
        public Task<int> CountPropertiesAsync();
        public Task<int> CountPendingPropertiesAsync();
        public Task<int> CountAcceptedPropertiesAsync();
        public Task<int> CountRejectedPropertiesAsync();
        public Task<Dictionary<string, int>> GetPropertiesByTypeAsync();
        public Task<Dictionary<string, int>> GetPropertiesByStatusAsync();
    }
}
