using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;

namespace Eskon.Infrastructure.Interfaces
{
    public interface ICountryRepository : IGenericRepositoryAsync<Country>
    {
        public Task<Country?> GetCountryByNameAsync(string name);

    }
}
