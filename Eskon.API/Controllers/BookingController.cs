using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.BookingFeatures.Commands.Command;
using Eskon.Domian.DTOs.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public BookingController(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region Actions
        [Authorize]
        [HttpPost("Customer")]
        public async Task<IActionResult> MakeABookingRequest([FromBody] BookingRequestDTO bookingWriteDTO)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new AddNewBookingCommand(userId, bookingWriteDTO));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpPatch("Owner/Accept/{bookingId:guid}")]
        public async Task<IActionResult> AcceptBooking([FromRoute] Guid bookingId)
        {
            var ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetBookingAsAcceptedCommand(bookingId, ownerId));
            return NewResult(response);
        }
        #endregion
    }
}
