using Eskon.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Service.Interfaces
{
    public interface ICityService
    {
        public Task<City?> AddCityAsync(City city);
        public Task<List<City>> GetAllCitiesAsync();
        public Task<City> GetCityByNameAsync(string name);
        public Task<City> GetCityByIdAsync(Guid id);
        public Task UpdateCityAsync(City city);
        public Task DeleteCityAsync(City city);



    }
}
