using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
