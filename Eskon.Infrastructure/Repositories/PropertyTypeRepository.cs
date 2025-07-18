using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Eskon.Infrastructure.Repositories
{
    class PropertyTypeRepository:GenericRepositoryAsync<PropertyType>,IPropertyTypeRepository
    {
        #region Fields
        private readonly DbSet<PropertyType> _PropertyTypeDbSet;
        #endregion

        #region Constructors
        public PropertyTypeRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _PropertyTypeDbSet = myDbContext.Set<PropertyType>();
        }

        public async Task<PropertyType> GetPropertyTypeByNameAsync(string name)
        {
            return await _PropertyTypeDbSet.SingleOrDefaultAsync(x=>x.Name==name);
        }
        #endregion

    }
}
