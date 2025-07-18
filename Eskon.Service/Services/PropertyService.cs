using System.ComponentModel.DataAnnotations;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository propertyRepository ;
        public PropertyService(IPropertyRepository PropertyRepo) {

            propertyRepository = PropertyRepo ;
        }
        public async Task<Property> AddPropertyAsync(Property property)
        {
            return await propertyRepository.AddAsync(property);
        }

        public async Task SetAverageRatingAsync(Guid PropertyId)
        {
            await propertyRepository.SetAverageRatingAsync(PropertyId);
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await propertyRepository.GetAllAsync();
        }

        public Task<List<Property>> GetPropertyByAdminIdAsync(Guid AdminId)
        {
            return propertyRepository.GetPropertyByAdminIdAsync(AdminId);
        }

        public async Task<List<Property>> getPropertybyCityAsync(string City, string Country)
        {
            return await propertyRepository.getPropertybyCityAsync(City, Country);
        }

        public async Task<Property> GetPropertyByIdAsync(Guid PropertyId)
        {
            return await propertyRepository.GetByIdAsync(PropertyId);
        }

        public async Task<List<Property>> GetPropertyByOwnerIdAsync(Guid OwnerId)
        {
            return await propertyRepository.GetPropertyByOwnerIdAsync(OwnerId);
        }

        public async Task<List<Property>> getPropertybyPriceRangAsync(int Min, int Max)
        {
            return await propertyRepository.getPropertybyPriceRangAsync(Min, Max);
        }

        public async Task<List<Property>> getPropertybyRatingAsync(int Rating)
        {
            return await propertyRepository.getPropertybyRatingAsync(Rating);
        }

        public async Task RemovePropertyAsync(Property property)
        {
            await propertyRepository.DeleteAsync(property);
        }

        public async Task SaveChangesAsync()
        {
            await propertyRepository.SaveChangesAsync();
        }


        public async Task SoftRemovePropertyAsync(Property property)
        {
            await propertyRepository.SoftDeleteAsync(property);
        }

        public async Task UpdateIsAcceptedPropertyAsync(Guid PropertyId)
        {
            Property AcceptedProperty =await GetPropertyByIdAsync(PropertyId);
            AcceptedProperty.IsAccepted = true;
        }

        public async Task UpdateIsSuspendedPropertyAsync(Guid PropertyId, bool value)
        {
            Property AcceptedProperty = await GetPropertyByIdAsync(PropertyId);
            AcceptedProperty.IsSuspended = value;
        }

        public async Task UpdatePropertyAsync(Property property)
        {
            await propertyRepository.UpdateAsync(property);
        }

        public async Task UpdateRejectionMessageAsync(Guid PropertyId, string Message)
        {
            Property RefusedProperty = await GetPropertyByIdAsync(PropertyId);
            RefusedProperty.RejectionMessage = Message;
        }
    }
}
