using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.PropertyFeatures.Commands.Command;
using Eskon.Core.Features.PropertyFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
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

        #region Endpoints

        #region GET
        #region Get filtered active properties
        /// <summary>
        /// Retrieves a paginated list of filtered active properties.
        /// </summary>
        /// <param name="minPricePerNight">Minimum price per night filter (optional).</param>
        /// <param name="maxPricePerNight">Maximum price per night filter (optional).</param>
        /// <param name="cityName">City name filter (optional).</param>
        /// <param name="countryName">Country name filter (optional).</param>
        /// <param name="Guests">Minimum number of guests required (optional).</param>
        /// <param name="SortBy">The field to sort by (e.g., "Price", "Title", "Rating").</param>
        /// <param name="asc">Indicates whether to sort in ascending order (default is false).</param>
        /// <param name="pageNum">Page number for pagination (default is 1).</param>
        /// <param name="itemsPerPage">Number of items per page (default is 10).</param>
        /// <returns>
        /// Returns a paginated list of property summaries that match the filters and sort order.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(typeof(Response<Paginated<PropertySummaryDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilteredActivePropertiesPaginated(
            [FromQuery] decimal? minPricePerNight,
            [FromQuery] decimal? maxPricePerNight,
            [FromQuery] string? cityName,
            [FromQuery] string? countryName,
            [FromQuery] int? Guests,
            [FromQuery] string? SortBy,
            [FromQuery] bool asc = false,
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
                Guests = Guests,
                SortBy = SortBy,
                Asc = asc
            };

            var response = await Mediator.Send(new GetFilteredActivePropertiesPaginated(pageNum, itemsPerPage, propertySearchFilters));
            return NewResult(response);
        }
        #endregion

        #region Get property by Id
        /// <summary>
        /// Retrieves the details of a property by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the property.</param>
        /// <returns>
        /// Returns the detailed information of the property if found; otherwise, a not found response.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response<PropertyDetailsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<PropertyDetailsDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPropertyById([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new GetPropertyByIdQuery(id));
            return NewResult(response);
        }
        #endregion

        #region Get Assigned pending properties
        /// <summary>
        /// Retrieves a paginated list of pending properties assigned to the currently authenticated admin.
        /// </summary>
        /// <param name="pageNum">The page number for pagination (default is 1).</param>
        /// <param name="itemsPerPage">The number of items per page (default is 10).</param>
        /// <returns>
        /// Returns a paginated list of <see cref="PropertySummaryDTO"/> representing pending properties assigned to the admin.
        /// </returns>
        /// <response code="200">Returns the paginated list of assigned pending properties.</response>
        /// <response code="401">Unauthorized access if the user is not authenticated.</response>
        /// <response code="403">Forbidden if the user does not have the Admin role.</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/Pending")]
        [ProducesResponseType(typeof(Response<Paginated<PropertySummaryDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAssignedPendingProperties([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            var adminId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new GetAssignedPendingPropertiesQuery(adminId, pageNum, itemsPerPage));
            return NewResult(response);
        }
        #endregion

        #region Get Active properties for currently auth owner
        /// <summary>
        /// Retrieves a paginated list of active properties for the currently authenticated owner.
        /// </summary>
        /// <param name="pageNum">The page number for pagination (default is 1).</param>
        /// <param name="itemsPerPage">The number of items to include per page (default is 10).</param>
        /// <returns>
        /// Returns a paginated list of <see cref="PropertySummaryDTO"/> for the authenticated owner.
        /// </returns>
        /// <response code="200">Returns the paginated list of active properties.</response>
        /// <response code="401">Unauthorized if the user is not authenticated.</response>
        /// <response code="403">Forbidden if the user does not have the Owner role.</response>
        [Authorize(Roles = "Owner")]
        [HttpGet("Owner")]
        [ProducesResponseType(typeof(Response<Paginated<PropertySummaryDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetActivePropertiesPerOwner([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new GetActivePropertiesPerOwnerQuery(ownerId, pageNum, itemsPerPage));
            return NewResult(response);
        }
        #endregion

        #region Get Pending properties 
        /// <summary>
        /// Retrieves a paginated list of pending properties submitted to the currently authenticated owner.
        /// </summary>
        /// <param name="pageNum">The current page number (default is 1).</param>
        /// <param name="itemsPerPage">The number of items to return per page (default is 10).</param>
        /// <returns>
        /// A paginated list of <see cref="PropertySummaryDTO"/> representing the pending properties.
        /// </returns>
        /// <response code="200">Returns the paginated list of pending properties.</response>
        /// <response code="401">Unauthorized if the user is not authenticated.</response>
        /// <response code="403">Forbidden if the user does not have the Owner role.</response>
        [Authorize(Roles = "Owner")]
        [HttpGet("Owner/pending")]
        [ProducesResponseType(typeof(Response<Paginated<PropertySummaryDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetPendingPropertiesPerOwner([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new GetPendingPropertiesPerOwnerQuery(ownerId, pageNum, itemsPerPage));
            return NewResult(response);
        }
        #endregion

        #region Get Suspended properties
        /// <summary>
        /// Retrieves a paginated list of suspended properties for the currently authenticated owner.
        /// </summary>
        /// <param name="pageNum">The page number to retrieve (default is 1).</param>
        /// <param name="itemsPerPage">The number of items per page (default is 10).</param>
        /// <returns>
        /// A paginated <see cref="PropertySummaryDTO"/> list of suspended properties.
        /// </returns>
        /// <response code="200">Returns the paginated list of suspended properties.</response>
        /// <response code="401">Unauthorized if the user is not authenticated.</response>
        /// <response code="403">Forbidden if the user is not in the Owner role.</response>
        [Authorize(Roles = "Owner")]
        [HttpGet("Owner/Suspended")]
        [ProducesResponseType(typeof(Response<Paginated<PropertySummaryDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetSuspendedPropertiesPerOwner([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new GetSuspendedPropertiesPerOwnerQuery(ownerId, pageNum, itemsPerPage));
            return NewResult(response);
        }
        #endregion

        #region Get Rejected properties
        /// <summary>
        /// Retrieves a paginated list of rejected properties for the currently authenticated owner.
        /// </summary>
        /// <param name="pageNum">The page number to retrieve (default is 1).</param>
        /// <param name="itemsPerPage">The number of items per page (default is 10).</param>
        /// <returns>
        /// A paginated list of <see cref="PropertySummaryDTO"/> representing rejected properties.
        /// </returns>
        /// <response code="200">Returns the paginated list of rejected properties.</response>
        /// <response code="401">Unauthorized if the user is not authenticated.</response>
        /// <response code="403">Forbidden if the user is not in the Owner role.</response>
        [Authorize(Roles = "Owner")]
        [HttpGet("Owner/Rejected")]
        [ProducesResponseType(typeof(Response<Paginated<PropertySummaryDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetRejectedPropertiesPerOwner([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            Guid ownerId = GetUserIdFromAuthenticatedUserToken();
            var response = await Mediator.Send(new GetRejectedPropertiesPerOwnerQuery(ownerId, pageNum, itemsPerPage));
            return NewResult(response);
        }
        #endregion 
        #endregion

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
