using Eskon.Domian.DTOs.BookingDTOs;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.BookingMapping
{
    public partial class BookingMappings
    {
        public void BookingReadMapping()
        {
            CreateMap<Booking, BookingReadDTO>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + ' ' + src.Customer.LastName))
            .ReverseMap();
        }
    }
}
