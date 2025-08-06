using System.Linq.Expressions;
using Eskon.Domain.Utilities;
using Eskon.Domian.Models;
using Eskon.Domian.Utilities;
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

        public async Task<Paginated<Property>> GetFilteredActivePropertiesPaginatedAsync(int pageNum, int itemsPerPage, PropertySearchFilters psf)
        {

            Expression<Func<Property, dynamic>> sort = null;

            switch (psf.SortBy?.ToUpperInvariant())
            {
                case "RATING":
                    sort = p => p.AverageRating;
                    break;

                case "CREATEDATE":
                    sort = p => p.CreatedAt;
                    break;

                case "PRICE":
                    sort = p => p.PricePerNight;
                    break;
            }

            return await propertyRepository.GetPaginatedSortedAsync(
                sort,
                psf.Asc,
                pageNum,
                itemsPerPage,
                filter: p => p.IsAccepted &&
                             !p.IsSuspended &&
                             ((psf.CityName != null) ? p.City.Name == psf.CityName : true) &&
                             ((psf.CountryName != null) ? p.City.Country.Name == psf.CountryName : true) &&
                             ((psf.Guests != null) ? p.MaxGuests >= psf.Guests : true) &&
                             ((psf.minPricePerNight != null) ? p.PricePerNight >= psf.minPricePerNight : true) &&
                             ((psf.maxPricePerNight != null) ? p.PricePerNight <= psf.maxPricePerNight : true)
                );
        }

        public async Task<List<Property>> GetPropertiesbyCityandCountryAsync(string City, string Country)
        {
            return await propertyRepository.GetFilteredAsync(x => x.City.Name == City && x.City.Country.Name == Country);
        }

        public async Task<Property> GetPropertyByIdAsync(Guid PropertyId)
        {
            return await propertyRepository.GetByIdAsync(PropertyId);
        }

        public async Task<Paginated<Property>> GetActivePropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            return await propertyRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: p => p.OwnerId == ownerId && p.IsAccepted && !p.IsSuspended);
        }

        public async Task<Paginated<Property>> GetPendingPropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            return await propertyRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: p => p.OwnerId == ownerId && p.IsPending);
        }

        public async Task<Paginated<Property>> GetSuspendedPropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            return await propertyRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: p => p.OwnerId == ownerId && p.IsSuspended);
        }

        public async Task<Paginated<Property>> GetRejectedPropertiesPerOwnerAsync(Guid ownerId, int pageNum, int itemsPerPage)
        {
            return await propertyRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: p => p.OwnerId == ownerId && !p.IsAccepted && !p.IsPending);
        }

        public async Task<Paginated<Property>> GetAssignedPendingPropertiesAsync(Guid adminId, int pageNum, int itemsPerPage)
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

        public async Task SetPropertyAsPendingAsync(Property property)
        {
            property.IsPending = true;
            property.IsAccepted = false;
            property.RejectionMessage = string.Empty;
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
