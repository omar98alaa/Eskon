using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Infrastructure.Interfaces.Country_City
{
    public interface ICountryRepository 
    {
        public Task<Country?> GetCountryByNameAsync(string name);
        public Task<Country?> AddAsync(Country country);
        public Task<List<Country>> GetAllAsync();
        public Task<Country> GetByIdAsync(Guid id);
    
        public Task UpdateAsync(Country country);
        public Task DeleteAsync(Country country);
        public Task AddRangeAsync(List<Country> countries);
        public Task<int> SaveChangesAsync();

    }
}
