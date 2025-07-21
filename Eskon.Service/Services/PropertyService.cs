using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository propertyRepository;
        public PropertyService(IPropertyRepository PropertyRepo)
        {

            propertyRepository = PropertyRepo;
        }
        public async Task<Property> AddPropertyAsync(Property property)
        {
            return await propertyRepository.AddAsync(property);
        }

        public async Task SetAverageRatingAsync(Property property, decimal AverageRating)
        {
            property.AverageRating = AverageRating;
            await propertyRepository.UpdateAsync(property);
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await propertyRepository.GetAllAsync();
        }

        public async Task<List<Property>> GetPendingPropertiesAsync(Guid AdminId)
        {
            return await propertyRepository.GetFilteredAsync(x => x.AssignedAdminId == AdminId && x.IsPending == true);
        }

        public async Task<List<Property>> GetPropertiesbyCityandCountryAsync(string City, string Country)
        {
            return await propertyRepository.GetFilteredAsync(x => x.City.Name == City && x.City.Country.Name == Country);
        }

        public async Task<Property> GetPropertyByIdAsync(Guid PropertyId)
        {
            return await propertyRepository.GetByIdAsync(PropertyId);
        }

        public async Task<List<Property>> GetPropertiesPerOwnerAsync(Guid OwnerId)
        {
            return await propertyRepository.GetFilteredAsync(x => x.OwnerId == OwnerId);
        }

        public async Task<List<Property>> GetActivePropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            return await propertyRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: p => p.OwnerId == ownerId && p.IsAccepted && !p.IsSuspended);
        }

        public async Task<List<Property>> GetPendingPropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            return await propertyRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: p => p.OwnerId == ownerId && p.IsPending);
        }

        public async Task<List<Property>> GetSuspendedPropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            return await propertyRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: p => p.OwnerId == ownerId && p.IsSuspended);
        }

        public async Task<List<Property>> GetRejectedPropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            return await propertyRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: p => p.OwnerId == ownerId && !p.IsAccepted && !p.IsPending);
        }

        public async Task<List<Property>> GetAssignedPendingPropertiesAsync(Guid adminId, int pageNum, int itemsPerPage)
        {
            return await propertyRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: p => p.AssignedAdminId == adminId && p.IsPending);
        }

        public async Task<List<Property>> GetPropertiesbyPriceRangAsync(decimal MinPricePerNight, decimal MaxPricePerNight)
        {
            return await propertyRepository.GetFilteredAsync(x => x.PricePerNight >= MinPricePerNight && x.PricePerNight <= MaxPricePerNight);
        }

        public async Task<List<Property>> GetPropertiesbyRatingAsync(decimal Rating)
        {
            return await propertyRepository.GetFilteredAsync(x => x.AverageRating == Rating);
        }

        public async Task RemovePropertyAsync(Property property)
        {
            await propertyRepository.SoftDeleteAsync(property);
        }

        public async Task SetIsAcceptedPropertyAsync(Property property)
        {
            property.IsAccepted = true;
            property.IsPending = false;
            property.RejectionMessage = "";
            await propertyRepository.UpdateAsync(property);
        }

        public async Task SetPropertySuspensionStateAsync(Property property, bool value)
        {
            property.IsSuspended = value;
            await propertyRepository.UpdateAsync(property);
        }

        public async Task UpdatePropertyAsync(Property property)
        {
            await propertyRepository.UpdateAsync(property);
        }

        public async Task SetRejectionMessageAsync(Property property, string RejectionMessage)
        {
            property.RejectionMessage = RejectionMessage;
            property.IsPending = false;
            property.IsAccepted = false;
            await propertyRepository.UpdateAsync(property);
        }
    }
}
