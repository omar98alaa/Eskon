using Eskon.Domian.DTOs.BookingDTOs;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.BookingMapping
{
    public partial class BookingMapping
    {
        public void CreateBooking()
        {
            CreateMap<Booking, BookingReadDTO>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt)).ReverseMap();
        }

        public void CheckIfAlreadyBooked()
        {
            CreateMap<Booking, BookingRequestDTO>().ReverseMap();
        }
    }
}
