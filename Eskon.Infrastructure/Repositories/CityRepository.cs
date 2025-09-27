using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eskon.Infrastructure.Repositories
{
    public class CityRepository : GenericRepositoryAsync<City>, ICityRepository
    {
        #region Fields
        private readonly MyDbContext _myDbContext;
        #endregion

        #region Constructors
        public CityRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _myDbContext = myDbContext;
        }
        #endregion

        #region Handle Functions

        public async Task<City?> GetCityByNameAsync(string name)
        {
            return await _myDbContext.Cities.FirstOrDefaultAsync(c => c.Name == name);
        }
        #endregion
    }
}
