using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class CityService : ICityService
    {
        #region Fields
        private readonly ICityRepository _cityRepository;
        #endregion
        #region Constructors
        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        #endregion
        #region Handles Functions
        public async Task<City?> AddCityAsync(City city)
        {
            return await _cityRepository.AddAsync(city);
        }
        public async Task<List<City>> GetAllCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<List<City>> GetAllCitiesPerCountryAsync(Country country)
        {
            return await _cityRepository.GetFilteredAsync(c => c.CountryId == country.Id);
        }

        public async Task<City> GetCityByIdAsync(Guid id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }

        public async Task<City> GetCityByNameAsync(string name)
        {
            return await _cityRepository.GetCityByNameAsync(name);
        }

        public async Task UpdateCityAsync(City city)
        {
            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteCityAsync(City city)
        {
            await _cityRepository.DeleteAsync(city);
        }


        #endregion
    }
}
