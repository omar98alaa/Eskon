using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    interface IPropertyTypeService
    {
        public Task<List<PropertyType>> GetPropertyTypesAsync();
        public Task<PropertyType> AddPropertyType(PropertyType PropertyType);
        public Task<PropertyType> GetPropertyTypeByNameAsync(string name);
        public Task UpdatePropertyType(PropertyType propertyType);
        public Task RemovePropertyAsync(PropertyType property);
        public Task SoftRemovePropertyAsync(PropertyType property);
        public Task SaveChangesAsync();
    }
}
