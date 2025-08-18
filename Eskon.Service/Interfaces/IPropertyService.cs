using Eskon.Domain.Utilities;
using Eskon.Domian.Models;
using Eskon.Domian.Utilities;

namespace Eskon.Service.Interfaces
{
    public interface IPropertyService
    {
        #region Read
        public Task<List<Property>> GetAllPropertiesAsync();
        public Task<Property> GetPropertyByIdAsync(Guid propertyId);
        public Task<Paginated<Property>> GetFilteredActivePropertiesPaginatedAsync(int pageNum, int itemsPerPage, PropertySearchFilters propertySearchFilters);
        public Task<Paginated<Property>> GetActivePropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Property>> GetPendingPropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Property>> GetSuspendedPropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Property>> GetRejectedPropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage);
        public Task<Paginated<Property>> GetAssignedPendingPropertiesAsync(Guid adminId, int pageNum, int itemsPerPage);
        public Task<List<Property>> GetPropertiesbyCityandCountryAsync(string city, string country);

        public Task<List<Property>> GetPropertiesbyRatingAsync(decimal Rating);

        public Task<List<Property>> GetPropertiesbyPriceRangAsync(decimal minPricePerNight, decimal maxPricePerNight);
        public Task<List<Property>> GetAllPendingPropertiesPerAdminAsync(Guid assignedAdmin);
        #endregion
        #region Add
        public Task<Property> AddPropertyAsync(Property property);
        #endregion
        #region Update
        public Task UpdatePropertyAsync(Property property);
        public Task SetPropertySuspensionStateAsync(Property property, bool value);
        public Task SetIsAcceptedPropertyAsync(Property property);
        public Task SetPropertyAsPendingAsync(Property property);
        public Task SetRejectionMessageAsync(Property property, String regectionMessage);
        public Task SetAverageRatingAsync(Property property, decimal averageRating);
        #endregion
        #region Delete
        public Task RemovePropertyAsync(Property property);
        #endregion
    }
}
