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
            /*.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.User.FirstName + src.User.LastName))
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Property.Owner.FirstName + src.Property.Owner.LastName));*/

            #endregion

            #region WriteMapping
            #endregion


            ;
        }
        
    }
}
