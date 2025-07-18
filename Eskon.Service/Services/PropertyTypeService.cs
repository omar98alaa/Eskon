using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;
using Eskon.Infrastructure.Repositories;

namespace Eskon.Service.Services
{
    class PropertyTypeService : IPropertyTypeService
    {
        private readonly IPropertyTypeRepository propertyTypeRepository;
        public PropertyTypeService(IPropertyTypeRepository PropertyTypeRepo)
        {

        propertyTypeRepository = PropertyTypeRepo;
        }
        public async Task<PropertyType> GetPropertyTypeByNameAsync(string name)
        {
           return await propertyTypeRepository.GetPropertyTypeByNameAsync(name);
        }
        public async Task<PropertyType> AddPropertyType(PropertyType propertyType)
        {
            return await propertyTypeRepository.AddAsync(propertyType);
        }

        public async Task<List<PropertyType>> GetPropertyTypesAsync()
        {
            return await propertyTypeRepository.GetAllAsync();
        }

        public async Task RemovePropertyAsync(PropertyType propertyType)
        {
            await propertyTypeRepository.DeleteAsync(propertyType);
        }

        public async Task SaveChangesAsync()
        {
            await propertyTypeRepository.SaveChangesAsync();
        }

        public async Task SoftRemovePropertyAsync(PropertyType propertyType)
        {
            await propertyTypeRepository.SoftDeleteAsync(propertyType);
        }

        public async Task UpdatePropertyType(PropertyType propertyType)
        {
            await propertyTypeRepository.UpdateAsync(propertyType);
        }
    }
}
