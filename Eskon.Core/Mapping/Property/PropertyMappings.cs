using AutoMapper;

namespace Eskon.Core.Mapping.Users
{
    public partial class PropertyMappings : Profile
    {
        public PropertyMappings()
        {
            PropertyDetailsMapping();
            PropertySummaryMapping();
        }

    }
}
