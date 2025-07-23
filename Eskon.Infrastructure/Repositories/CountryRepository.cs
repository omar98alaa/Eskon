using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Infrastructure.Repositories
{
    public class CountryRepository : GenericRepositoryAsync<Country>,ICountryRepository
    {
        #region Fields
        private readonly DbSet<Country> _countryDbSet;
        #endregion

        #region Constructors
        public CountryRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _countryDbSet = myDbContext.Set<Country>();
        }
        #endregion

        #region Handles Function

        public async Task<Country?> GetCountryByNameAsync(string name)
        {
            return await _myDbContext.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }

        #endregion
    }
}
