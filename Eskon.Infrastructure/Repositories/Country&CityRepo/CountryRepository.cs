using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Interfaces.Country_City;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Infrastructure.Repositories.Country_CityRepo
{
    public class CountryRepository : ICountryRepository
    {
        #region Fields
        private readonly MyDbContext _myDbContext;
        #endregion

        #region Constructors
        public CountryRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        #endregion

        #region Handles Function
        public async Task<Country?> AddAsync(Country country)
        {
            await _myDbContext.Countries.AddAsync(country);
            return country;
        }
        public async Task<List<Country>> GetAllAsync()
        {
            return await _myDbContext.Countries.ToListAsync();
        }
        public async Task<Country> GetByIdAsync(Guid id)
        {
            return await _myDbContext.Countries.FindAsync(id);
        }
   
        //public async Task<Country?> GetCountryByNameAsync(string name)
        //{
        //    return await _myDbContext.Countries.FirstOrDefaultAsync(c => c.Name == name);
        //}

        public async Task UpdateAsync(Country country)
        {
            _myDbContext.Countries.Update(country);
            await _myDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Country country)
        {
            _myDbContext.Countries.Remove(country);
            await _myDbContext.SaveChangesAsync();
        }
        public async Task AddRangeAsync(List<Country> countries)
        {
            await _myDbContext.Countries.AddRangeAsync(countries);
            await _myDbContext.SaveChangesAsync();
        }


        public async Task<Country?> GetCountryByNameAsync(string name)
        {
            return await _myDbContext.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _myDbContext.SaveChangesAsync();
        }
        #endregion
    }
}
