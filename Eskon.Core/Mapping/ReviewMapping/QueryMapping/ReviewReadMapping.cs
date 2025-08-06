using Eskon.Domian.DTOs.ReviewDTOs;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.ReviewMapping
{
    public partial class ReviewMappings
    {
        public void ReviewReadMapping()
        {
            CreateMap<Review, ReviewReadDTO>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + ' ' + src.Customer.LastName))
            .ForMember(dest => dest.PropertyTitle, opt => opt.MapFrom(src => src.Property.Title))
            .ReverseMap();
        }
    }
}
