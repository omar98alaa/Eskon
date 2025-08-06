using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.BookingFeatures.Commands.Command;
using Eskon.Core.Features.ReviewFeatures.Commands.Command;
using Eskon.Core.Features.ReviewFeatures.Queries.Query;
using Eskon.Core.Features.StripeFeatures.Commands.Command;
using Eskon.Domian.DTOs.ReviewDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ReviewController(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region GET
        [HttpGet("{propertyId:guid}")]
        public async Task<IActionResult> GetReviewsPerProperty([FromRoute] Guid propertyId, [FromQuery] int pageNum, [FromQuery] int itemsPerPage)
        {
            var query = new GetReviewsPerPropertyQuery(propertyId, pageNum, itemsPerPage);
            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        [Authorize]
        [HttpGet("Customer")]
        public async Task<IActionResult> GetReviewsPerCustomer([FromQuery] int pageNum, [FromQuery] int itemsPerPage)
        {
            var customerId = GetUserIdFromAuthenticatedUserToken();
            var query = new GetReviewsPerCustomerQuery(customerId, pageNum, itemsPerPage);
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
        #endregion

        #region POST
        [Authorize]
        [HttpPost("{bookingId:guid}")]
        public async Task<IActionResult> AddReview([FromRoute] Guid bookingId, ReviewWriteDTO reviewWriteDTO)
        {
            var customerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new AddReviewCommand(customerId, bookingId, reviewWriteDTO));
            return NewResult(response);
        }
        #endregion

        #region PUT
        [Authorize]
        [HttpPut("{reviewId:guid}")]
        public async Task<IActionResult> EditReview([FromRoute] Guid reviewId, ReviewWriteDTO reviewWriteDTO)
        {
            var customerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new EditReviewCommand(customerId, reviewId, reviewWriteDTO));
            return NewResult(response);
        }
        #endregion

        #region DELETE
        [Authorize]
        [HttpDelete("{reviewId:guid}")]
        public async Task<IActionResult> DeleteReview([FromRoute] Guid reviewId)
        {
            var customerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new DeleteReviewCommand(customerId, reviewId));
            return NewResult(response);
        }
        #endregion
    }
}
