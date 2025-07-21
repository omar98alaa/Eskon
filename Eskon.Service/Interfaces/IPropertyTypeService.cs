using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IPropertyTypeService
    {
        public Task<List<PropertyType>> GetPropertyTypesAsync();
        public Task<PropertyType> AddPropertyType(PropertyType PropertyType);
        public Task UpdatePropertyType(PropertyType propertyType);
        public Task RemovePropertyAsync(PropertyType property);
    }
}
