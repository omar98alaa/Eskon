using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface ICityRepository : IGenericRepositoryAsync<City>
    {
        public Task<City?> GetCityByNameAsync(string name);
    }
}
