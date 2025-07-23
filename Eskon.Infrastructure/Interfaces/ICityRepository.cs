using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Infrastructure.Interfaces
{
    public interface ICityRepository : IGenericRepositoryAsync<City>
    {
        public Task<City?> GetCityByNameAsync(string name);
       // public Task<City?> AddAsync(City city);
       // public Task<List<City>> GetAllAsync();
       // public Task<City> GetByIdAsync(Guid id);
       // public Task UpdateAsync(City city);
       // public Task DeleteAsync(City city);
       // public Task AddRangeAsync(List<City> cities);
 


    }
}
