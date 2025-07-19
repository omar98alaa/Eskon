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
        public Task<Property> GetPropertyByIdAsync(Guid propertyId);
        public Task<List<Property>> GetPropertiesPerOwnerAsync(Guid ownerId);
        public Task<List<Property>> GetPendingPropertiesAsync(Guid adminId);
        public Task<List<Property>> GetPropertiesbyCityandCountryAsync(string city, string country);

        public Task<List<Property>> GetPropertiesbyRatingAsync(decimal Rating);

        public Task<List<Property>> GetPropertiesbyPriceRangAsync(decimal minPricePerNight, decimal maxPricePerNight);
        #endregion
        #region Add
        public Task<Property> AddPropertyAsync(Property property);
        #endregion
        #region Update
        public Task UpdatePropertyAsync(Property property);
        public Task SetPropertySuspensionStateAsync(Property property,bool value);
        public Task SetIsAcceptedPropertyAsync(Property property);
        public Task SetRejectionMessageAsync(Property property, String regectionMessage);
        public Task SetAverageRatingAsync(Property property,decimal averageRating);
        #endregion
        #region Delete
        public Task RemovePropertyAsync(Property property);
        #endregion
        #region SaveChanges
        public Task<int> SaveChangesAsync();
        #endregion
    }
}
