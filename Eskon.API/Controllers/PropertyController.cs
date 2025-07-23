using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.PropertyFeatures.Commands.Command;
using Eskon.Core.Features.PropertyFeatures.Queries.Query;
using Eskon.Domian.DTOs.Property;
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

        [Authorize(Roles = "Owner")]
        [HttpPost]
        public async Task<IActionResult> AddProperty(PropertyWriteDTO propertyWriteDTO)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new AddPropertyCommand(ownerId, propertyWriteDTO));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpPatch("Owner/Suspend/{id}")]
        public async Task<IActionResult> SetIsSuspendedProperty([FromRoute]Guid id,bool value)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetIsSuspendedPropertyCommand(id,value,ownerId));
            return NewResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Admin/Accept/{id}")]
        public async Task<IActionResult> SetPropertyAsAccepted([FromRoute] Guid id)
        {
            Guid AdminId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetPropertyAsAcceptedCommand(id, AdminId));
            return NewResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Admin/Reject/{id}")]
        public async Task<IActionResult> SetPropertyAsRejected([FromRoute] Guid id,string RejectionMessage)
        {
            Guid AdminId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetPropertyAsRejectedCommand(id, RejectionMessage, AdminId));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("Owner/{id}")]
        public async Task<IActionResult> UpdateProperty([FromRoute] Guid id, PropertyWriteDTO propertyWriteDTO)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new UpdatePropertyCommand(id,propertyWriteDTO,ownerId));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("Owner/{id}")]
        public async Task<IActionResult> DeleteProperty([FromRoute] Guid id)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new DeletePropertyCommand(id, ownerId));
            return NewResult(response);
        }
        #endregion
    }
}
