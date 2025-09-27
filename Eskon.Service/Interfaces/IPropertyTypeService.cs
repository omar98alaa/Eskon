using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IPropertyTypeService
    {
        public Task<List<PropertyType>> GetPropertyTypesAsync();
        public Task<PropertyType> GetPropertyTypesByIdAsync(Guid id);
        public Task<PropertyType> AddPropertyType(PropertyType PropertyType);
        public Task UpdatePropertyType(PropertyType propertyType);
        public Task RemovePropertyAsync(PropertyType property);
    }
}
