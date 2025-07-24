using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.PropertyFeatures.Commands.Command;
using Eskon.Core.Features.PropertyFeatures.Queries.Query;
using Eskon.Domian.DTOs.Property;
using Eskon.Domian.Utilities;
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
        [HttpGet]
        public async Task<IActionResult> GetFilteredActivePropertiesPaginated(
            [FromQuery] decimal? minPricePerNight,
            [FromQuery] decimal? maxPricePerNight,
            [FromQuery] string? cityName,
            [FromQuery] string? countryName,
            [FromQuery] int? Guests,
            [FromQuery] int pageNum = 1,
            [FromQuery] int itemsPerPage = 10
            )
        {
            var propertySearchFilters = new PropertySearchFilters()
            {
                minPricePerNight = minPricePerNight,
                maxPricePerNight = maxPricePerNight,
                CityName = cityName,
                CountryName = countryName,
                Guests = Guests
            };

            var response = await Mediator.Send(new GetFilteredActivePropertiesPaginated(pageNum, itemsPerPage, propertySearchFilters));
            return NewResult(response);
        }

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
        [HttpPatch("Owner/Suspend/{propertyId:guid}")]
        public async Task<IActionResult> SetPropertySuspensionState([FromRoute]Guid propertyId,[FromQuery]bool state)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetIsSuspendedPropertyCommand(propertyId,state,ownerId));
            return NewResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Admin/Accept/{propertyId:guid}")]
        public async Task<IActionResult> SetPropertyAsAccepted([FromRoute] Guid propertyId)
        {
            Guid AdminId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetPropertyAsAcceptedCommand(propertyId, AdminId));
            return NewResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Admin/Reject/{propertyId:guid}")]
        public async Task<IActionResult> SetPropertyAsRejected([FromRoute] Guid propertyId, string RejectionMessage)
        {
            Guid AdminId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new SetPropertyAsRejectedCommand(propertyId, RejectionMessage, AdminId));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("Owner/{propertyId:guid}")]
        public async Task<IActionResult> UpdateProperty([FromRoute] Guid propertyId, PropertyWriteDTO propertyWriteDTO)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new UpdatePropertyCommand(propertyId, propertyWriteDTO,ownerId));
            return NewResult(response);
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("Owner/{propertyId:guid}")]
        public async Task<IActionResult> DeleteProperty([FromRoute] Guid propertyId)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new DeletePropertyCommand(propertyId, ownerId));
            return NewResult(response);
        }
        #endregion
    }
}
