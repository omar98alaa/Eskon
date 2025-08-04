using Eskon.Domian.DTOs.BookingDTOs;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.BookingMapping
{
    public partial class BookingMappings
    {
        public void BookingRequestMapping()
        {
            CreateMap<Booking, BookingRequestDTO>().ReverseMap();
        }
    }
}
