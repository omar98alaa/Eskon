using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IPropertyService
    {
        #region Read
        public Task<List<Property>> GetAllPropertiesAsync();
        public Task<Property> GetPropertyByIdAsync(Guid PropertyId);
        public Task<List<Property>> GetPropertyByOwnerIdAsync(Guid OwnerId);
        public Task<List<Property>> GetPropertyByAdminIdAsync(Guid AdminId);
        public Task<List<Property>> getPropertybyCityAsync(string City, string Country);

        public Task<List<Property>> getPropertybyRatingAsync(int Rating);

        public Task<List<Property>> getPropertybyPriceRangAsync(int Min, int Max);
        #endregion
        #region Add
        public Task<Property> AddPropertyAsync(Property property);
        #endregion
        #region Update
        public Task UpdatePropertyAsync(Property property);
        public Task UpdateIsSuspendedPropertyAsync(Guid PropertyId,bool value);
        public Task UpdateIsAcceptedPropertyAsync(Guid PropertyId);
        public Task UpdateRejectionMessageAsync(Guid PropertyId,String Message);
        public Task SetAverageRatingAsync(Guid PropertyId);
        #endregion
        #region Delete
        public Task RemovePropertyAsync(Property property);
        public Task SoftRemovePropertyAsync(Property property);
        #endregion
        #region SaveChanges
        public Task SaveChangesAsync();
        #endregion
    }
}
