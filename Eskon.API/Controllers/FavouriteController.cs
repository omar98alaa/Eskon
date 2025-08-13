using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.FavouriteFeatures.Commands.Command;
using Eskon.Core.Features.FavouriteFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Favourite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public FavouriteController(IMapper mapper)
        {
            _mapper = mapper;
        }
        #endregion

        #region GET
        #region Get user favourites
        /// <summary>
        /// Retrieves a paginated list of the authenticated user's favourite properties.
        /// </summary>
        /// <param name="pageNum">The page number to retrieve (default is 1).</param>
        /// <param name="itemsPerPage">The number of items per page (default is 10).</param>
        /// <returns>
        /// Returns a paginated list of <see cref="FavouriteReadDTO"/> objects for the authenticated user.
        /// </returns>
        /// <response code="200">Returns a paginated list of the user's favourites.</response>
        /// <response code="401">Unauthorized — authentication required.</response>
        [Authorize]
        [HttpGet("GetUserFavourites")]
        [ProducesResponseType(typeof(Response<Paginated<FavouriteReadDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<Paginated<FavouriteReadDTO>>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserFavourites([FromQuery] int pageNum = 1, [FromQuery] int itemsPerPage = 10)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var query = new GetUserFavouritesQuery(pageNum, itemsPerPage, userId);
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
        #endregion
        #endregion

        #region POST
        /// <summary>
        /// Adds a property to the authenticated user's favourites list.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint allows an authenticated user to add a property to their list of favourites.  
        /// The property must exist, and it must not already be in the user's favourites list.
        ///
        /// **Workflow:**  
        /// 1. Extracts the authenticated user's ID from the JWT token.  
        /// 2. Validates that the specified property exists.  
        /// 3. Checks if the property is already in the user's favourites list.  
        /// 4. If not, creates a new favourite entry linking the user to the property.  
        /// 5. Saves the change and returns the newly created favourite as a DTO.  
        ///
        /// **Example Request:**
        /// ```http
        /// POST /AddToFavourites/5f9c0b8b-1234-4f7a-8a6e-9876543210ab
        /// Authorization: Bearer {jwt_token}
        /// ```
        ///
        /// **Example Successful Response:**
        /// ```json
        /// {
        ///   "succeeded": true,
        ///   "message": "Property added to Favourites Succefully",
        ///   "errors": null,
        ///   "data": {
        ///     "id": "c0a80123-4567-890a-bcde-f1234567890a",
        ///     "propertyId": "5f9c0b8b-1234-4f7a-8a6e-9876543210ab",
        ///     "userId": "d1a80123-4567-890a-bcde-f1234567890b"
        ///   }
        /// }
        /// ```
        ///
        /// **Route Parameters:**  
        /// - `propertyId` *(Guid, required)* — The ID of the property to add to favourites.
        ///
        /// **Possible Error Responses:**  
        /// - `401 Unauthorized` — User is not authenticated.  
        /// - `404 Not Found` — Property not found.  
        /// - `400 Bad Request` — Property is already in the user's favourites list.  
        ///
        /// **Notes:**  
        /// - Only authenticated users can add favourites.  
        /// - Duplicate entries are prevented by a pre-check before insertion.  
        /// </remarks>
        /// <param name="propertyId">The GUID of the property to be added to the user's favourites.</param>
        /// <returns>Returns the newly created favourite as a <see cref="FavouriteReadDTO"/>.</returns>
        /// <response code="201">Property successfully added to the favourites list.</response>
        /// <response code="400">Property is already in the user's favourites list.</response>
        /// <response code="401">Unauthorized — authentication required.</response>
        /// <response code="404">Property not found.</response>
        [Authorize]
        [HttpPost("{propertyId:guid}")]
        [ProducesResponseType(typeof(Response<FavouriteReadDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<FavouriteReadDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<FavouriteReadDTO>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<FavouriteReadDTO>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddToFavourites([FromRoute] Guid propertyId)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var command = new AddFavouriteCommand(userId,propertyId);
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Removes a property from the authenticated user's favourites list.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint allows an authenticated user to remove a property from their favourites list.  
        /// It ensures the favourite exists and that it belongs to the authenticated user before deletion.
        ///
        /// **Workflow:**  
        /// 1. Extracts the authenticated user's ID from the JWT token.  
        /// 2. Retrieves the favourite by its unique ID.  
        /// 3. Returns `404 Not Found` if the favourite does not exist.  
        /// 4. Checks that the favourite belongs to the authenticated user; otherwise returns `403 Forbidden`.  
        /// 5. Deletes the favourite record from the database.  
        /// 6. Returns a success message confirming the removal.  
        ///
        /// **Example Request:**
        /// ```http
        /// DELETE /RemoveFromFavourites/c0a80123-4567-890a-bcde-f1234567890a
        /// Authorization: Bearer {jwt_token}
        /// ```
        ///
        /// **Example Successful Response:**
        /// ```json
        /// {
        ///   "succeeded": true,
        ///   "message": "Favourite successfully removed",
        ///   "errors": null,
        ///   "data": "Favourite successfully removed"
        /// }
        /// ```
        ///
        /// **Route Parameters:**  
        /// - `favouriteId` *(Guid, required)* — The ID of the favourite entry to remove.
        ///
        /// **Possible Error Responses:**  
        /// - `401 Unauthorized` — User is not authenticated.  
        /// - `403 Forbidden` — The favourite belongs to another user.  
        /// - `404 Not Found` — Favourite not found in the user's list.  
        ///
        /// **Notes:**  
        /// - Only the owner of the favourite can remove it.  
        /// - Deletion is permanent and cannot be undone.  
        /// </remarks>
        /// <param name="favouriteId">The GUID of the favourite to be removed from the user's list.</param>
        /// <returns>Returns a success message upon successful removal.</returns>
        /// <response code="200">Favourite successfully removed.</response>
        /// <response code="401">Unauthorized — authentication required.</response>
        /// <response code="403">Forbidden — favourite does not belong to the authenticated user.</response>
        /// <response code="404">Favourite not found.</response>
        [Authorize]
        [HttpDelete("{favouriteId:guid}")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFromFavourites([FromRoute] Guid favouriteId)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var command = new RemoveFavouriteCommand(userId, favouriteId);
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        #endregion


    }
}
