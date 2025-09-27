using AutoMapper;

namespace Eskon.Core.Mapping.ReviewMapping
{
    public partial class ReviewMappings : Profile
    {
        public ReviewMappings()
        {
            ReviewWriteMapping();
            ReviewReadMapping();
        }
    }
}
