
using Eskon.Domian.DTOs.Property;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.Users
{
    public partial class PropertyMappings
    {
        public void PropertyDetailsMapping()
        {
            // Source -> Destination
            CreateMap<Property, PropertyDetailsDTO>()
                .AfterMap((src, dest) =>
                {
                    dest.OwnerName = src.Owner.FirstName + ' ' + src.Owner.LastName;
                    dest.PropertyType = src.PropertyType.Name;
                    dest.City = src.City.Name;
                    dest.Country = src.City.Country.Name;
                    dest.ImageURLs = src.Images.Select(i => i.Url).ToList();
                });
        }
    }
}
