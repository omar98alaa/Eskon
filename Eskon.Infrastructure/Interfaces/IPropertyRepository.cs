using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IPropertyRepository:IGenericRepositoryAsync<Property>
    {
        Task<int> CountPropertiesAsync();
        Task<int> CountPendingPropertiesAsync();
        Task<int> CountAcceptedPropertiesAsync();
        Task<int> CountRejectedPropertiesAsync();
        Task<Dictionary<string, int>> GetPropertiesByTypeAsync();
        Task<Dictionary<string, int>> GetPropertiesByStatusAsync();

    }
}
