using Eskon.Domian.DTOs.Property;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.Properties
{
    public partial class PropertyMappings
    {
        public void AddPropertyMapping()
        {
            // Source -> Destination

            CreateMap<PropertyWriteDTO, Property>();
        }
    }
}
