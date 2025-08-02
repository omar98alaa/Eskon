using AutoMapper;
using Eskon.Domian.DTOs.BookingDTOs;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.BookingMapping
{
    public class BookingProfileMapping : Profile
    {
        public BookingProfileMapping()
        {
            #region ReadMapping
            CreateMap<Booking, BookingReadDTO>();
            #endregion

            #region WriteMapping
            #endregion
        }
        
    }
}
