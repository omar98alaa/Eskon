using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Domian.DTOs.BookingDTOs;
using Eskon.Domian.DTOs.StripeDTOs;
using Eskon.Core.Features.BookingFeatures.Queries.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eskon.Core.Features.BookingFeatures.Commands.Command;

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

        #region POST
        [Authorize]
        [HttpGet("GetCustomerBookings")]
        public async Task<IActionResult> GetCustomerBookings([FromQuery] string status, [FromQuery] int pageNum, [FromQuery] int itemsPerPage)
        {
            var customerId = GetUserIdFromAuthenticatedUserToken();
            var query = new GetCustomerBookingsQuery(customerId, status, pageNum, itemsPerPage);
            var bookings = await Mediator.Send(query);
            return NewResult(bookings);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("GetOwnerBookings")]
        public async Task<IActionResult> GetOwnerBookings([FromQuery] string status, [FromQuery] int pageNum, [FromQuery] int itemsPerPage)
        {
            var ownerId = GetUserIdFromAuthenticatedUserToken();
            var query = new GetOwnerBookingsQuery(ownerId, status, pageNum, itemsPerPage);
            var bookings = await Mediator.Send(query);
            return Ok(bookings);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("GetPropertyBookings")]
        public async Task<IActionResult> GetPropertyBookings([FromQuery] string status, [FromQuery] int pageNum, [FromQuery] int itemsPerPage)
        {
            var ownerId = GetUserIdFromAuthenticatedUserToken();
            var query = new GetPropertyBookingsQuery(ownerId, status, pageNum, itemsPerPage);
            var bookings = await Mediator.Send(query);
            return Ok(bookings);
        }

        [Authorize]
        [HttpPost("Customer/Request")]
        public async Task<IActionResult> MakeABookingRequest([FromBody] BookingRequestDTO bookingWriteDTO)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new AddNewBookingCommand(userId, bookingWriteDTO));
            return NewResult(response);

        }
        #endregion

        #region PATCH
        [Authorize(Roles = "Owner")]
        [HttpPatch("Owner/Accept/{bookingId:guid}")]
        public async Task<IActionResult> AcceptBooking([FromRoute] Guid bookingId)
        {
            var ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetBookingAsAcceptedCommand(bookingId, ownerId));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpPatch("Owner/Reject/{bookingId:guid}")]
        public async Task<IActionResult> RejectBooking([FromRoute] Guid bookingId)
        {
            var ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetBookingAsRejectedCommand(bookingId, ownerId));
            return NewResult(response);
        }

        [Authorize]
        [HttpPatch("Customer/Pay/{bookingId:guid}")]
        public async Task<IActionResult> PayBooking([FromRoute] Guid bookingId, CreateStripeCheckoutRequestDTO createStripeCheckoutRequestDTO)
        {
            var cusotmerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new CreateStripeCheckoutLinkCommand(bookingId, cusotmerId, createStripeCheckoutRequestDTO));
            return NewResult(response);
        }
        #endregion

        #region DELETE
        [Authorize]
        [HttpDelete("Customer/Cancel/{bookingId:guid}")]
        public async Task<IActionResult> CancelBooking([FromRoute] Guid bookingId)
        {
            var customerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new CancelBookingCommand(bookingId, customerId));
            return NewResult(response);
        }

        [Authorize]
        [HttpDelete("Customer/Refund/{bookingId:guid}")]
        public async Task<IActionResult> RefundBooking([FromRoute] Guid bookingId)
        {
            var cusotmerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new CreateStripePaymentRefundCommand(cusotmerId, bookingId));
            return NewResult(response);
        }
        #endregion
    }
}
