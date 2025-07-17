using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces.Country_City;
using Eskon.Infrastructure.Repositories.Country_CityRepo;
using Eskon.Service.Interfaces.Country_City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Service.Services.Country_City
{
    public class CountryService :ICountryService
    {
        #region Fields
        private readonly ICountryRepository _countryRepository;
        #endregion

        #region Constructors
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        #endregion

        #region Handles Functions
        public async Task<Country?> AddCountryAsync(Country country)
        {
            return await _countryRepository.AddAsync(country);
        }
        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            return await _countryRepository.GetByIdAsync(id);
        }

        public async Task<Country?> GetCountryByNameAsync(string name)
        {
            return await _countryRepository.GetCountryByNameAsync(name);
        }

        public async Task<List<Country>> GetCountryListAsync()
        {
            return await _countryRepository.GetAllAsync();
        }


        public async Task UpdateCountryAsync(Country country)
        {
           await _countryRepository.UpdateAsync(country);
        }

        public async Task DeleteCountryAsync(Country country)
        {
           await _countryRepository.DeleteAsync(country);
        }

        public async Task AddCountryAsync(List<Country> countries)
        {
           await _countryRepository.AddRangeAsync(countries);


        }
        public async Task<int> SaveChangesAsync()
        {
            return await _countryRepository.SaveChangesAsync();
        }



        #endregion
    }
}
