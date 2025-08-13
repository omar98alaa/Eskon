using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.BookingFeatures.Commands.Command;
using Eskon.Core.Features.BookingFeatures.Queries.Query;
using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.BookingDTOs;
using Eskon.Domian.DTOs.StripeDTOs;
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

        #region POST
        #region GetCustomerBookings
        /// <summary>
        /// Retrieves paginated bookings for the authenticated customer based on booking status.
        /// </summary>
        /// <param name="status">The booking status (e.g., pending, accepted, paid, history, rejected).</param>
        /// <param name="pageNum">The page number (starting from 1).</param>
        /// <param name="itemsPerPage">The number of bookings per page.</param>
        /// <returns>A paginated list of customer bookings.</returns>
        [Authorize]
        [HttpGet("GetCustomerBookings")]
        [ProducesResponseType(typeof(Response<Paginated<BookingReadDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCustomerBookings(
            [FromQuery] string status,
            [FromQuery] int pageNum,
            [FromQuery] int itemsPerPage)
        {
            var customerId = GetUserIdFromAuthenticatedUserToken();
            var query = new GetCustomerBookingsQuery(customerId, status, pageNum, itemsPerPage);
            var bookings = await Mediator.Send(query);
            return NewResult(bookings);
        }
        #endregion

        #region Get owner bookings
        /// <summary>
        /// Retrieves paginated bookings for the authenticated property owner based on booking status.
        /// </summary>
        /// <param name="status">The booking status (e.g., pending, accepted, paid, history, rejected).</param>
        /// <param name="pageNum">The page number (starting from 1).</param>
        /// <param name="itemsPerPage">The number of bookings per page.</param>
        /// <returns>A paginated list of owner bookings.</returns>
        [Authorize(Roles = "Owner")]
        [HttpGet("GetOwnerBookings")]
        [ProducesResponseType(typeof(Response<Paginated<BookingReadDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetOwnerBookings([FromQuery] string status, [FromQuery] int pageNum, [FromQuery] int itemsPerPage)
        {
            var ownerId = GetUserIdFromAuthenticatedUserToken();
            var query = new GetOwnerBookingsQuery(ownerId, status, pageNum, itemsPerPage);
            var bookings = await Mediator.Send(query);
            return Ok(bookings);
        }
        #endregion

        #region Get Property Bookings
        /// <summary>
        /// Retrieves a paginated list of bookings for all properties owned by the authenticated owner,
        /// filtered by booking status.
        /// </summary>
        /// <param name="status">
        /// The booking status to filter by. Accepted values:
        /// <c>pending</c>, <c>accepted</c>, <c>paid</c>, <c>history</c>, <c>rejected</c>.
        /// </param>
        /// <param name="pageNum">The page number to retrieve (must be ≥ 1).</param>
        /// <param name="itemsPerPage">The number of bookings to include per page.</param>
        /// <returns>
        /// Returns <c>200 OK</c> with a paginated list of bookings for the owner's properties.
        /// Returns <c>400 Bad Request</c> if the status is invalid.
        /// Returns <c>401 Unauthorized</c> if the owner is not authenticated.
        /// </returns>
        [Authorize(Roles = "Owner")]
        [HttpGet("GetPropertyBookings")]
        [ProducesResponseType(typeof(Response<Paginated<BookingReadDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPropertyBookings([FromQuery] string status, [FromQuery] int pageNum, [FromQuery] int itemsPerPage)
        {
            var ownerId = GetUserIdFromAuthenticatedUserToken();
            var query = new GetPropertyBookingsQuery(ownerId, status, pageNum, itemsPerPage);
            var bookings = await Mediator.Send(query);
            return Ok(bookings);
        }
        #endregion

        #region Add New Booking
        /// <summary>
        /// Creates a new booking request for a property by the authenticated customer.
        /// </summary>
        /// <param name="bookingWriteDTO">The booking request data including property ID, start and end dates, etc.</param>
        /// <returns>
        /// Returns <c>201 Created</c> if the booking request is valid and successfully submitted.
        /// Returns <c>400 Bad Request</c> if the booking request is invalid, overlaps with existing bookings, or violates business rules.
        /// Returns <c>404 Not Found</c> if the specified property does not exist.
        /// Returns <c>401 Unauthorized</c> if the user is not authenticated.
        /// </returns>
        [Authorize]
        [HttpPost("Customer/Request")]
        [ProducesResponseType(typeof(Response<BookingReadDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MakeABookingRequest([FromBody] BookingRequestDTO bookingWriteDTO)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new AddNewBookingCommand(userId, bookingWriteDTO));
            return NewResult(response);

        }
        #endregion
        #endregion

        #region PATCH
        #region Accept pending booking
        /// <summary>
        /// Accepts a pending booking request for one of the authenticated owner's properties.
        /// </summary>
        /// <param name="bookingId">The ID of the booking to accept.</param>
        /// <returns>
        /// Returns <c>200 OK</c> if the booking is accepted successfully and overlapping bookings are handled.
        /// Returns <c>400 Bad Request</c> if the booking is not pending or if the reservation overlaps with another accepted booking.
        /// Returns <c>403 Forbidden</c> if the authenticated owner does not own the property.
        /// Returns <c>404 Not Found</c> if the booking does not exist.
        /// Returns <c>401 Unauthorized</c> if the user is not authenticated as an owner.
        /// </returns>
        [Authorize(Roles = "Owner")]
        [HttpPatch("Owner/Accept/{bookingId:guid}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AcceptBooking([FromRoute] Guid bookingId)
        {
            var ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetBookingAsAcceptedCommand(bookingId, ownerId));
            return NewResult(response);
        }
        #endregion

        #region Reject pending booking
        /// <summary>
        /// Rejects a pending booking request for one of the authenticated owner's properties.
        /// </summary>
        /// <param name="bookingId">The ID of the booking to reject.</param>
        /// <returns>
        /// Returns <c>200 OK</c> if the booking was successfully rejected.
        /// Returns <c>400 Bad Request</c> if the booking is not in a pending state.
        /// Returns <c>403 Forbidden</c> if the authenticated user does not own the property associated with the booking.
        /// Returns <c>404 Not Found</c> if the booking does not exist.
        /// Returns <c>401 Unauthorized</c> if the user is not authenticated as an owner.
        /// </returns>
        [Authorize(Roles = "Owner")]
        [HttpPatch("Owner/Reject/{bookingId:guid}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RejectBooking([FromRoute] Guid bookingId)
        {
            var ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetBookingAsRejectedCommand(bookingId, ownerId));
            return NewResult(response);
        }
        #endregion

        #region Create Stripe checkout and pay booking
        /// <summary>
        /// Initiates the Stripe payment process for a booking using a checkout session.
        /// </summary>
        /// <param name="bookingId">The unique identifier of the booking to be paid for.</param>
        /// <param name="createStripeCheckoutRequestDTO">The request body containing Stripe success and cancel URLs.</param>
        /// <returns>
        /// Returns <c>200 OK</c> with the Stripe Checkout URL if the session is successfully created.<br/>
        /// Returns <c>400 Bad Request</c> if the booking is not accepted, invalid, or the DTO fails validation.<br/>
        /// Returns <c>403 Forbidden</c> if the booking does not belong to the authenticated customer.<br/>
        /// Returns <c>404 Not Found</c> if the booking is not found.
        /// </returns>
        [Authorize]
        [HttpPatch("Customer/Pay/{bookingId:guid}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PayBooking([FromRoute] Guid bookingId, CreateStripeCheckoutRequestDTO createStripeCheckoutRequestDTO)
        {
            var cusotmerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new CreateStripeCheckoutLinkCommand(bookingId, cusotmerId, createStripeCheckoutRequestDTO));
            return NewResult(response);
        }
        #endregion
        #endregion

        #region DELETE
        #region Cancel Booking
        /// <summary>
        /// Cancels a booking made by the authenticated customer if it has not been paid.
        /// </summary>
        /// <param name="bookingId">The unique identifier of the booking to cancel.</param>
        /// <returns>
        /// Returns <c>200 OK</c> with a success message if the booking is successfully canceled.<br/>
        /// Returns <c>400 Bad Request</c> if the booking has already been paid.<br/>
        /// Returns <c>403 Forbidden</c> if the booking does not belong to the authenticated customer.<br/>
        /// Returns <c>404 Not Found</c> if the booking does not exist.
        /// </returns>
        [Authorize]
        [HttpDelete("Customer/Cancel/{bookingId:guid}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelBooking([FromRoute] Guid bookingId)
        {
            var customerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new CancelBookingCommand(bookingId, customerId));
            return NewResult(response);
        }
        #endregion

        #region Refund payed Booking
        /// <summary>
        /// Initiates a refund request for a paid booking, if eligible.
        /// </summary>
        /// <param name="bookingId">The unique identifier of the booking to refund.</param>
        /// <returns>
        /// Returns <c>200 OK</c> if the refund request is successfully created.<br/>
        /// Returns <c>400 Bad Request</c> if the booking is unpaid, already refunded, or the refund period has expired.<br/>
        /// Returns <c>403 Forbidden</c> if the booking does not belong to the authenticated customer.<br/>
        /// Returns <c>404 Not Found</c> if the booking or its associated payment does not exist.
        /// </returns>
        [Authorize]
        [HttpDelete("Customer/Refund/{bookingId:guid}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefundBooking([FromRoute] Guid bookingId)
        {
            var cusotmerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new CreateStripePaymentRefundCommand(cusotmerId, bookingId));
            return NewResult(response);
        } 
        #endregion
        #endregion
    }
}
