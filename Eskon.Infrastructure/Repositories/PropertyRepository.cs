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
    class PropertyRepository: GenericRepositoryAsync<Property>,IPropertyRepository
    {
        #region Fields
        private readonly DbSet<Property> _PropertyDbSet;
        #endregion

        #region Constructors
        public PropertyRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _PropertyDbSet = myDbContext.Set<Property>();
        }
        #endregion

        #region Methods
        public async Task<int> GetAllPendingPropertiesCountPerAdminAsync(Guid assignedAdmin)
        {
            return await _PropertyDbSet.Where(p => p.IsPending == true && p.AssignedAdminId == assignedAdmin).CountAsync();
        }
        #endregion
    }
}