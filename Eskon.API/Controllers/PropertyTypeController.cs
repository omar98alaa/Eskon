using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.PropertyTypeFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.PropertyType;
using Microsoft.AspNetCore.Mvc;
namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypeController : BaseController
    {
        #region Constructors
        public PropertyTypeController()
        {
        }
        #endregion

        #region Endpoints
        /// <summary>
        /// Retrieves all available property types.
        /// </summary>
        /// <remarks>
        /// This endpoint returns a list of all property types stored in the system.  
        /// 
        /// **Use Case:**
        /// - Intended for populating dropdowns, selection lists, or filters in property-related features.
        /// - Useful in property creation, search, or filtering workflows.
        /// 
        /// **Workflow:**
        /// 1. Sends a <see cref="GetAllPropertyTypesQuery"/> request through MediatR.
        /// 2. The query handler fetches all property types from the <c>PropertyTypeService</c>.
        /// 3. Maps the property type entities into <see cref="PropertyTypeDTO"/> objects.
        /// 4. Returns the list wrapped in a <see cref="Response{T}"/>.
        /// 
        /// **Example Request:**
        /// ```http
        /// GET /GetAllPropertyTypes
        /// Authorization: Bearer {jwt_token}
        /// ```
        /// 
        /// **Example Successful Response:**
        /// ```json
        /// {
        ///   "succeeded": true,
        ///   "message": null,
        ///   "errors": null,
        ///   "data": [
        ///     { "id": "b3a5e0f2-1d3b-4e6a-9cb2-123456789abc", "name": "Apartment" },
        ///     { "id": "a6d4e8c7-5f9b-4d5f-8e3a-987654321def", "name": "Villa" }
        ///   ]
        /// }
        /// ```
        /// </remarks>
        /// <returns>
        /// A list of <see cref="PropertyTypeDTO"/> objects containing the ID and name of each property type.
        /// </returns>
        /// <response code="200">Returns the list of property types successfully.</response>
        [HttpGet("GetAllPropertyTypes")]
        [ProducesResponseType(typeof(Response<List<PropertyTypeDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPropertyTypes()
        {
            var result = await Mediator.Send(new GetAllPropertyTypesQuery());
            return NewResult(result);
        } 
        #endregion

    }
}
