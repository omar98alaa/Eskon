using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.PropertyFeatures.Queries.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public PropertyController(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region Controllers
        // Get All

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetPropertyByIdQuery(id));
            return NewResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/Pending")]
        public async Task<IActionResult> GetAssignedPendingProperties([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            var adminId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new GetAssignedPendingPropertiesQuery(adminId, pageNum, itemsPerPage));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("Owner")]
        public async Task<IActionResult> GetActivePropertiesPerOwner([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new GetActivePropertiesPerOwnerQuery(ownerId, pageNum, itemsPerPage));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("Owner/pending")]
        public async Task<IActionResult> GetPendingPropertiesPerOwner([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new GetPendingPropertiesPerOwnerQuery(ownerId, pageNum, itemsPerPage));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("Owner/Suspended")]
        public async Task<IActionResult> GetSuspendedPropertiesPerOwner([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new GetSuspendedPropertiesPerOwnerQuery(ownerId, pageNum, itemsPerPage));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("Owner/Rejected")]
        public async Task<IActionResult> GetRejectedPropertiesPerOwner([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new GetRejectedPropertiesPerOwnerQuery(ownerId, pageNum, itemsPerPage));
            return NewResult(response);
        }
        #endregion
    }
}
