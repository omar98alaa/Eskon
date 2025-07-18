using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IPropertyRepository:IGenericRepositoryAsync<Property>
    {
        public Task<List<Property>> getPropertybyCityAsync(string City, string Country);

        public Task<List<Property>> getPropertybyRatingAsync(int Rating);

        public Task<List<Property>> getPropertybyPriceRangAsync(int Min, int Max);

        public Task<List<Property>> GetPropertyByOwnerIdAsync(Guid OwnerId);
        public Task<List<Property>> GetPropertyByAdminIdAsync(Guid AdminId);
        public Task SetAverageRatingAsync(Guid PropertyId);
    }
}
