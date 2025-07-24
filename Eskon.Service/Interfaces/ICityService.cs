using Eskon.Domian.Models;

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
