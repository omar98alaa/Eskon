using Eskon.Domian.DTOs.Property;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.Properties
{
    public partial class PropertyMappings
    {
        public void AddPropertyMapping()
        {
            // Source -> Destination

            CreateMap<PropertyWriteDTO, Property>().AfterMap((src, des) =>
            {
                foreach (string Url in src.Images) {

                    des.Images.Add(new Image() { Url = Url});
                };
                des.City=new City() { Name=src.City,Country=new Country() { Name=src.Country} };
            });
        }
    }
}
