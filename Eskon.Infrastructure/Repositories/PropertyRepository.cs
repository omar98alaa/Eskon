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
        public Task<int> CountPendingPropertiesAsync()
        {
            return _PropertyDbSet.CountAsync(p => p.IsPending == true);
        }

        public Task<int> CountPropertiesAsync()
        {
            return _PropertyDbSet.CountAsync();
        }

        public Task<int> CountAcceptedPropertiesAsync()
        {
            return _PropertyDbSet.CountAsync(p => p.IsAccepted == true);
        }

        public Task<int> CountRejectedPropertiesAsync()
        {
            return _PropertyDbSet.CountAsync(p => p.RejectionMessage != null);
        }
        public async Task<Dictionary<string, int>> GetPropertiesByTypeAsync()
        {
            return await _PropertyDbSet
                .GroupBy(p => p.PropertyType.Name)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Type, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetPropertiesByStatusAsync()
        {
            return await _PropertyDbSet
                .GroupBy(p =>
                    p.IsSuspended ? "Suspended" :
                    (p.IsPending ? "Pending" :
                    (p.IsAccepted ? "Accepted" : "Rejected"))
                )
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }
    }
}