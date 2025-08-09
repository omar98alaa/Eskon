using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IPropertyTypeRepository propertyTypeRepository;
        public PropertyTypeService(IPropertyTypeRepository PropertyTypeRepo)
        {

            propertyTypeRepository = PropertyTypeRepo;
        }

        public async Task<PropertyType> AddPropertyType(PropertyType propertyType)
        {
            return await propertyTypeRepository.AddAsync(propertyType);
        }

        public async Task<List<PropertyType>> GetPropertyTypesAsync()
        {
            return await propertyTypeRepository.GetAllAsync();
        }

        public async Task<PropertyType> GetPropertyTypesByIdAsync(Guid id)
        {
            return await propertyTypeRepository.GetByIdAsync(id);
        }

        public async Task RemovePropertyAsync(PropertyType propertyType)
        {
            await propertyTypeRepository.SoftDeleteAsync(propertyType);
        }

        public async Task UpdatePropertyType(PropertyType propertyType)
        {
            await propertyTypeRepository.UpdateAsync(propertyType);
        }
    }
}
