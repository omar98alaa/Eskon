using AutoMapper;

namespace Eskon.Core.Mapping.BookingMapping
{
    public partial class BookingMappings : Profile
    {
        public BookingMappings()
        {
            BookingRequestMapping();
            BookingReadMapping();
        }
    }
}
