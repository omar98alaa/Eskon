
using Eskon.Domian.DTOs.Property;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.Users
{
    public partial class PropertyMappings
    {
        public void PropertySummaryMapping()
        {
            // Source -> Destination
            CreateMap<Property, PropertySummaryDTO>()
                .AfterMap((src, dest) =>
                {
                    dest.City = src.City.Name;
                    dest.Country = src.City.Country.Name;
                });
        }
    }
}
