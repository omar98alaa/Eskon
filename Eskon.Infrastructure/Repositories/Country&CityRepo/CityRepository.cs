using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Interfaces.Country_City;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Infrastructure.Repositories.Country_CityRepo
{
    public class CityRepository : ICityRepository
    {
        #region Fields
        private readonly MyDbContext _myDbContext;
        #endregion

        #region Constructors
        public CityRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        #endregion

        #region Handle Functions
        public async Task<City?> AddAsync(City city)
        {
            await _myDbContext.Cities.AddAsync(city);
            return city;
        }
        public async Task<List<City>> GetAllAsync()
        {
            return await _myDbContext.Cities.ToListAsync();
        }
        public async Task<City> GetByIdAsync(Guid id)
        {
            return await _myDbContext.Cities.FindAsync(id);
        }
   

        public async Task<City?> GetCityByNameAsync(string name)
        {
            return await _myDbContext.Cities.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task UpdateAsync(City city)
        {
            _myDbContext.Cities.Update(city);
            await _myDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(City city)
        {
            _myDbContext.Cities.Remove(city);
            await _myDbContext.SaveChangesAsync();
        }
        public async Task AddRangeAsync(List<City> cities)
        {
            await _myDbContext.Cities.AddRangeAsync(cities);
            await _myDbContext.SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
         
                return await _myDbContext.SaveChangesAsync();
            
        
        }
        #endregion
    }
}
