using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.Country_CityFeatures.Commands.Commands;
using Eskon.Core.Features.CountryFeatures.Queries.Models;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.CountryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        #region Fields
        #endregion

        #region Constructors
        public CountryController()
        {
        }
        #endregion

        #region Endpoints
        #region GET
        #region GET all countries
        /// <summary>
        /// Retrieves the complete list of countries.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint returns a list of all countries available in the system.  
        /// It queries the database for all country records, maps them to DTOs, and returns them in the response.
        ///
        /// **Workflow:**  
        /// 1. Sends a `GetCountryListQuery` to the MediatR pipeline.  
        /// 2. The query handler fetches the list of countries from the `CountryService`.  
        /// 3. Maps each entity to a `CountryReadDTO`.  
        /// 4. Returns the list wrapped in a success response.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /GetAllCountries
        /// ```
        ///
        /// **Example Successful Response:**
        /// ```json
        /// {
        ///   "succeeded": true,
        ///   "message": null,
        ///   "errors": null,
        ///   "data": [
        ///     { "id": "b3f8e5e1-6c9f-4a42-b5a2-1b3c4eab1234", "name": "United States" },
        ///     { "id": "9a7c38f5-0a41-4a5f-a4f5-8d5c3e0b1234", "name": "Canada" }
        ///   ]
        /// }
        /// ```
        ///
        /// **Returns:**  
        /// - A list of all countries in the database.
        ///
        /// **Possible Error Responses:**  
        /// - `500 Internal Server Error` — If a server-side exception occurs.
        ///
        /// **Notes:**  
        /// - No authentication is required for this endpoint (unless decorated with `[Authorize]`).  
        /// - The returned data is intended for populating dropdowns, forms, and selection lists.
        /// </remarks>
        /// <returns>List of countries as <see cref="CountryReadDTO"/> objects.</returns>
        /// <response code="200">Successfully retrieved the list of countries.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Response<List<CountryReadDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<CountryReadDTO>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCountries()
        {
            var result = await Mediator.Send(new GetCountryListQuery());
            return NewResult(result);
        }
        #endregion

        #region GET country by name
        /// <summary>
        /// Retrieves a specific country by its name.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint searches for a country by its exact name and returns its details if found.  
        /// If no matching country exists in the database, it returns a `404 Not Found` response.
        ///
        /// **Workflow:**  
        /// 1. Receives a country name from the route parameter.  
        /// 2. Sends a `GetCountryByNameQuery` to the MediatR pipeline.  
        /// 3. The handler calls `CountryService.GetCountryByNameAsync(name)` to fetch the matching country.  
        /// 4. If the country exists, it maps it to a `CountryDTO` and returns it.  
        /// 5. If not found, returns an error message with a `404` status.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /GetCountryByName/Egypt
        /// ```
        ///
        /// **Returns:**  
        /// - A single `CountryDTO` containing the matching country’s details.
        ///
        /// **Possible Error Responses:**  
        /// - `404 Not Found` — If no country with the given name exists.  
        /// - `500 Internal Server Error` — If a server-side exception occurs.
        ///
        /// **Notes:**  
        /// - The search is case-sensitive or case-insensitive depending on the implementation of `GetCountryByNameAsync`.  
        /// - Useful for validating user selections or displaying country-specific data.
        /// </remarks>
        /// <param name="name">The exact name of the country to retrieve.</param>
        /// <returns>A <see cref="CountryDTO"/> representing the requested country.</returns>
        /// <response code="200">Country found and returned successfully.</response>
        /// <response code="404">No country found with the provided name.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Response<CountryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CountryDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<CountryDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountryByName(string name)
        {
            var result = await Mediator.Send(new GetCountryByNameQuery(name));
            return NewResult(result);
        }
        #endregion
        #endregion

        #region POST
        /// <summary>
        /// Adds a new country to the system.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint allows authorized administrators to create a new country record in the database.  
        /// It requires the caller to have either the **Admin** or **Root** role.
        ///
        /// **Workflow:**  
        /// 1. Receives an `AddCountryCommand` containing the new country’s details in the request body.  
        /// 2. The handler creates a `Country` entity using the provided name.  
        /// 3. Calls `CountryService.AddCountryAsync` to insert the new record.  
        /// 4. Saves changes to the database.  
        /// 5. Maps the newly created entity to an `AddCountryDTO` and returns it with a success message.
        ///
        /// **Authorization:**  
        /// - Requires the `Admin` or `Root` policy to execute.  
        ///
        /// **Example Request:**
        /// ```http
        /// POST /AddCountry
        /// Content-Type: application/json
        ///
        /// {
        ///   "addCountryDTO": {
        ///     "name": "Egypt"
        ///   }
        /// }
        /// ```
        ///
        ///
        /// **Returns:**  
        /// - The newly created country as an `AddCountryDTO`.
        ///
        /// **Possible Error Responses:**  
        /// - `400 Bad Request` — If the input data is invalid.  
        /// - `403 Forbidden` — If the user is not authorized (not Admin or Root).  
        /// - `500 Internal Server Error` — If a server-side exception occurs.
        ///
        /// **Notes:**  
        /// - This endpoint does not check for duplicate country names — that should be handled in the service layer if required.
        /// </remarks>
        /// <param name="command">The command object containing the details of the country to be added.</param>
        /// <returns>The newly added country details wrapped in a response object.</returns>
        /// <response code="200">Country created successfully.</response>
        /// <response code="400">Invalid input data.</response>
        /// <response code="403">User does not have the required permissions.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize("Admin, Root")]
        [HttpPost]
        [ProducesResponseType(typeof(Response<AddCountryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<AddCountryDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<AddCountryDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<AddCountryDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddCountry([FromBody] AddCountryCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        #endregion

        #region PUT
        /// <summary>
        /// Updates the details of an existing country.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint allows authorized administrators to update the name of an existing country.  
        /// The user must have the **Admin** or **Root** role to access it.
        ///
        /// **Workflow:**  
        /// 1. Receives the `id` of the country to update as a route parameter.  
        /// 2. Accepts a `CountryUpdateDTO` in the request body containing the new country name.  
        /// 3. Checks if a country with the given name exists in the database.  
        /// 4. If found, updates its `Name` and sets the `UpdatedAt` timestamp to the current UTC time.  
        /// 5. Saves the changes and returns the updated country details.
        ///
        /// **Important Note:**  
        /// - The handler currently searches for the country **by name**, not by ID, which means:  
        ///   - If the new name is not already in the database, the method will return `NotFound`.  
        ///   - This may not behave as expected when using `id` for route matching.  
        ///
        /// **Authorization:**  
        /// - Requires `Admin` or `Root` role.
        ///
        /// **Example Request:**
        /// ```http
        /// PUT /countries/7b8b2f68-1234-4d23-8a1b-9876543210ab
        /// Content-Type: application/json
        ///
        /// {
        ///   "name": "United States of America"
        /// }
        /// ```
        ///
        /// **Possible Error Responses:**  
        /// - `404 Not Found` — If the specified country name does not exist in the system.  
        /// - `403 Forbidden` — If the caller does not have the required permissions.  
        /// - `500 Internal Server Error` — If an unhandled exception occurs.
        ///
        /// </remarks>
        /// <param name="id">The unique identifier (GUID) of the country to update.</param>
        /// <param name="dto">The updated country data.</param>
        /// <returns>The updated country details wrapped in a response object.</returns>
        /// <response code="200">Country updated successfully.</response>
        /// <response code="404">Country not found.</response>
        /// <response code="403">User not authorized.</response>
        /// <response code="500">Internal server error.</response>
        [Authorize("Admin, Root")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Response<CountryUpdateDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<CountryUpdateDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<CountryUpdateDTO>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<CountryUpdateDTO>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(Guid id, [FromBody] CountryUpdateDTO dto)
        {
            var result = await Mediator.Send(new EditCountryCommand(id, dto));
            return NewResult(result);
        }  
        #endregion
        #endregion
    }
}
