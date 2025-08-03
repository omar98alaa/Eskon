using AutoMapper;
using Eskon.Domian.DTOs.BookingDTOs;
using Eskon.Domian.Models;

namespace Eskon.Core.Mapping.BookingMapping
{
    public partial class BookingMapping : Profile
    {
        public BookingMapping()
        {
            #region ReadMapping
            CreateBooking();
            CheckIfAlreadyBooked();
            #endregion

            #region WriteMapping
            #endregion
        }
        
    }
}
