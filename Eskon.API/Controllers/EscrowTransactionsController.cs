using Eskon.API.Base;
using Eskon.Core.Features.EscrowTransactionFeatures.Commands.Command;
using Eskon.Core.Features.EscrowTransactionFeatures.Queries.Query;
using Eskon.Domian.DTOs.EscrowTransaction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscrowTransactionsController : BaseController
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public EscrowTransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        [HttpPost("AddEscrowTransaction")]
        public async Task<IActionResult> CreateEscrowTransaction([FromBody] AddEscrowTransactionDTO addEscrowTransactionDTO)
        {
            //addEscrowTransactionDTO.CustomerId =  GetUserIdFromAuthenticatedUserToken();
            var result = await _mediator.Send(new AddEscrowTransactionCommand(addEscrowTransactionDTO));
            return NewResult(result);
        }

        [HttpPost("capture")]
        public async Task<IActionResult> CapturePayment([FromBody] CapturePaymentDto dto)
        {
            var result = await _mediator.Send(new CapturePaymentCommand(dto));
            return NewResult(result);
        }

        [HttpPost("ReleaseToOwner")]
        public async Task<IActionResult> ReleaseToOwner([FromBody] Guid bookingId)
        {
            var result = await _mediator.Send(new ReleaseToOwnerCommand(bookingId));
            return NewResult(result);
        }

        [HttpPost("RefundToCustomer")]
        public async Task<IActionResult> RefundToCustomer([FromBody] Guid bookingId)
        {
            var result = await _mediator.Send(new RefundToCustomerCommand(bookingId));
            return NewResult(result);
        }

        [HttpGet("GetByBookingId/{bookingId:guid}")]
        public async Task<IActionResult> GetByBookingId([FromRoute] Guid bookingId)
        {
            var result = await _mediator.Send(new GetEscrowTransactionByBookingIdQuery(bookingId));
            return NewResult(result);
        }

        [HttpGet("getAllPendingReleases")]
        public async Task<IActionResult> GetAllPendingReleases()
        {
            var result = await _mediator.Send(new GetAllPendingEscrowReleasesQuery());
            return NewResult(result);
        }
    }
}
