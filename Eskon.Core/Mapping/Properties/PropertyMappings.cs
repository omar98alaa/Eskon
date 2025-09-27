using AutoMapper;

namespace Eskon.Core.Mapping.Properties
{
    public partial class PropertyMappings : Profile
    {
        public PropertyMappings()
        {
            PropertyDetailsMapping();
            PropertySummaryMapping();
            AddPropertyMapping();
        }

    }
}
