using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface ICountryService
    {
        public Task<Country?> GetCountryByNameAsync(string name);
        public Task<Country?> AddCountryAsync(Country country);
        public Task<List<Country>> GetCountryListAsync();
        public Task<Country> GetCountryByIdAsync(Guid id);
        public Task UpdateCountryAsync(Country country);
        public Task DeleteCountryAsync(Country country);



    }
}
