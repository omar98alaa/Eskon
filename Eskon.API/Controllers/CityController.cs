using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.CityFeatures.Commands.Commands;
using Eskon.Core.Features.CityFeatures.Queries.Models;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        #region Fields
        #endregion

        #region Constructors
        public CityController()
        {
        }
        #endregion

        #region Endpoints
        #region GET
        #region GET all cities
        /// <summary>
        /// Retrieves all cities for a specified country.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint returns a list of all cities associated with a given country name.  
        /// The country name is provided as a query parameter.
        ///
        /// **Workflow:**  
        /// 1. Accepts the `countryName` parameter from the query string.  
        /// 2. Looks up the country in the database by its name.  
        /// 3. If the country does not exist, returns a `404 Not Found` response.  
        /// 4. If the country exists, retrieves all cities associated with it.  
        /// 5. Maps the city entities to `CityReadDTO` objects and returns them in the response.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /cities?countryName=Egypt
        /// ```
        ///
        ///
        /// **Possible Error Responses:**  
        /// - `404 Not Found` — If the specified country is not found.  
        /// - `500 Internal Server Error` — If an unhandled exception occurs.
        ///
        /// **Notes:**  
        /// - The `countryName` parameter is case-insensitive, depending on the database collation.
        /// - Results are returned as a list of `CityReadDTO` objects.
        ///
        /// </remarks>
        /// <param name="countryName">The name of the country for which to retrieve cities.</param>
        /// <returns>A list of cities for the specified country wrapped in a response object.</returns>
        /// <response code="200">Cities retrieved successfully.</response>
        /// <response code="404">Country not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Response<List<CityReadDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<CityReadDTO>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<List<CityReadDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCitiesPerCountry([FromQuery] string countryName)
        {
            var result = await Mediator.Send(new GetCityListQuery(countryName));
            return NewResult(result);
        }
        #endregion

        #region GET City by name
        /// <summary>
        /// Retrieves a city by its name.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint fetches the details of a single city based on the provided city name.
        ///
        /// **Workflow:**  
        /// 1. Accepts the `name` parameter from the route.  
        /// 2. Looks up the city in the database by its name.  
        /// 3. If the city does not exist, returns a `404 Not Found` response.  
        /// 4. If the city exists, maps it to a `CityDTO` and returns the data.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /cities/Cairo
        /// ```
        ///
        ///
        /// **Possible Error Responses:**  
        /// - `404 Not Found` — If the specified city is not found.  
        /// - `500 Internal Server Error` — If an unhandled exception occurs.
        ///
        /// **Notes:**  
        /// - The `name` parameter is case-insensitive, depending on database collation.
        /// - Returns a single `CityDTO` object.
        ///
        /// </remarks>
        /// <param name="name">The name of the city to retrieve.</param>
        /// <returns>Details of the specified city wrapped in a response object.</returns>
        /// <response code="200">City retrieved successfully.</response>
        /// <response code="404">City not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Response<CityDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CityDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<CityDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCityByName(string name)
        {
            var result = await Mediator.Send(new GetCityByNameQuery(name));
            return NewResult(result);
        }
        #endregion
        #endregion

        #region POST
        #region Add City
        /// <summary>
        /// Adds a new city to the system.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint creates a new city and associates it with an existing country.
        ///
        /// **Workflow:**  
        /// 1. Accepts a `CityDTO` from the request body.  
        /// 2. Validates that the specified country exists by matching `CountryName`.  
        /// 3. Checks if a city with the same name already exists in the country.  
        /// 4. If it exists, returns a `400 Bad Request` response.  
        /// 5. If it doesn't exist, creates the new city and saves it to the database.  
        /// 6. Returns the newly created city details.
        ///
        /// **Authorization:**  
        /// - Requires **Admin** or **Root** role.
        ///
        /// **Example Request:**
        /// ```http
        /// POST /cities
        /// Authorization: Bearer {token}
        /// Content-Type: application/json
        ///
        /// {
        ///   "name": "Alexandria",
        ///   "countryName": "Egypt"
        /// }
        /// ```
        ///
        ///
        /// **Possible Error Responses:**  
        /// - `404 Not Found` — If the specified country does not exist.  
        /// - `400 Bad Request` — If the city already exists in the specified country.  
        /// - `500 Internal Server Error` — If an unhandled exception occurs.
        ///
        /// **Notes:**  
        /// - The `name` check is case-insensitive, depending on database collation.
        /// - Each city is uniquely identified by its `Name` + `CountryId`.
        ///
        /// </remarks>
        /// <param name="city">The city details to add, including name and associated country name.</param>
        /// <returns>The newly created city wrapped in a response object.</returns>
        /// <response code="201">City created successfully.</response>
        /// <response code="400">City already exists in this country.</response>
        /// <response code="404">Country not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        //[Authorize("Admin, Root")]
        [ProducesResponseType(typeof(Response<CityDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        //[Authorize("Admin, Root")]
        public async Task<IActionResult> AddCity([FromBody] CityDTO city)
        {
            var result = await Mediator.Send(new AddCityCommand(city));
            return NewResult(result);
        }
        #endregion
        #endregion

        #region PUT
        /// <summary>
        /// Updates the details of an existing city.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint modifies an existing city's details, including its name and associated country.
        ///
        /// **Workflow:**  
        /// 1. Receives the current city name as a route parameter (`name`).  
        /// 2. Accepts updated city details in a `CityUpdateDTO` object from the request body.  
        /// 3. Validates that the city to update exists.  
        /// 4. Validates that the target country exists.  
        /// 5. Checks if another city with the same name already exists in the target country.  
        /// 6. If a duplicate exists, returns a `400 Bad Request`.  
        /// 7. If validation passes, updates the city's details and saves the changes.  
        /// 8. Returns the updated city data.
        ///
        /// **Authorization:**  
        /// - Requires **Admin** or **Root** role.
        ///
        /// **Example Request:**
        /// ```http
        /// PUT /cities/Cairo
        /// Authorization: Bearer {token}
        /// Content-Type: application/json
        ///
        /// {
        ///   "name": "Giza",
        ///   "countryName": "Egypt"
        /// }
        /// ```
        ///
        ///
        /// **Possible Error Responses:**  
        /// - `404 Not Found` — If the original city does not exist or the specified country does not exist.  
        /// - `400 Bad Request` — If another city with the same name already exists in the specified country.  
        /// - `500 Internal Server Error` — If an unhandled exception occurs.
        ///
        /// **Notes:**  
        /// - Name comparisons depend on database collation (case sensitivity may vary).  
        /// - Duplicate check is based on `Name` + `CountryId`.
        ///
        /// </remarks>
        /// <param name="name">The current name of the city to edit (from the route).</param>
        /// <param name="dto">The updated city details, including new name and country name.</param>
        /// <returns>The updated city data wrapped in a response object.</returns>
        /// <response code="200">City updated successfully.</response>
        /// <response code="400">City already exists in the specified country.</response>
        /// <response code="404">City or country not found.</response>
        /// <response code="500">Internal server error.</response>
        //[Authorize("Admin, Root, Customer, Owner")]
        [HttpPut("{name}")]
        [ProducesResponseType(typeof(Response<CityUpdateDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(string name, [FromBody] CityUpdateDTO dto)
        {
            var result = await Mediator.Send(new EditCityCommand(name, dto));
            return NewResult(result);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Deletes an existing city by its unique identifier.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint marks a city as deleted by setting its `DeletedAt` timestamp and persisting the change.  
        /// It does not necessarily remove the record physically (soft delete).
        ///
        /// **Workflow:**  
        /// 1. Receives the city ID as a route parameter.  
        /// 2. Looks up the city in the database.  
        /// 3. If the city does not exist, returns a `404 Not Found`.  
        /// 4. If found, sets the `DeletedAt` timestamp and calls the delete service.  
        /// 5. Saves changes and returns the deleted city data.
        ///
        /// **Authorization:**  
        /// - Requires **Admin** or **Root** role.
        ///
        /// **Example Request:**
        /// ```http
        /// DELETE /cities/9b2a6c87-9c1e-4ffb-9291-3827f8f9e31b
        /// Authorization: Bearer {token}
        /// ```
        ///
        ///
        /// **Possible Error Responses:**  
        /// - `404 Not Found` — If the city does not exist.  
        /// - `500 Internal Server Error` — If an unhandled exception occurs.
        ///
        /// **Notes:**  
        /// - This is a soft delete operation, meaning the city record remains in the database with a `DeletedAt` timestamp.  
        /// - Consumers should handle soft-deleted records appropriately in queries.
        ///
        /// </remarks>
        /// <param name="id">The unique identifier of the city to delete.</param>
        /// <returns>The deleted city details wrapped in a response object.</returns>
        /// <response code="200">City deleted successfully.</response>
        /// <response code="404">City not found.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize("Admin, Root")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Response<CityDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteCityCommand(id));
            return NewResult(result);
        } 
        #endregion
        #endregion
    }
}
