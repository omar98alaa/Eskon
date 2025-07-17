using Eskon.Core.Features.Country_CityFeatures.Commands.Commands;
using Eskon.Core.Features.Country_CityFeatures.Queries.Models;
using Eskon.Core.Features.UserFeatures.Commands;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        private readonly IMediator _mediator;
        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetAllCities()
        {
            var response = await _mediator.Send(new GetCityListQuery());
            if (response == null || !response.Any())
            {
                return NotFound(new { message = "No Cities found." });
            }
            return Ok(response);
        }

        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetCityByName(string name)
        {
            var response = await _mediator.Send(new GetCityByNameQuery { Name = name });
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddCity([FromBody] AddCityCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _mediator.Send(command);
            return response.Succeeded
                ? Ok(response.Data)
                : BadRequest(response.Errors);
        }
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCity(Guid id, [FromBody] City city)
        //{
        //    if (id != city.Id)
        //        return BadRequest();

        //    await _cityService.UpdateCityAsync(city);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCity(Guid id)
        //{
        //    await _cityService.DeleteCityAsync(id);
        //    return NoContent();
        //}

    }
}
