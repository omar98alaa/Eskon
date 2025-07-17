using Eskon.Core.Features.Country_CityFeatures.Commands.Commands;
using Eskon.Core.Features.Country_CityFeatures.Queries.Models;
using Eskon.Domian.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {

        private readonly IMediator _mediator;
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpGet("/Countries/List")]
        public async Task<IActionResult> GetAllCountries()
        {
            var response = await _mediator.Send(new GetCountryListQuery());
            if (response == null || !response.Any())
            {
                return NotFound(new { message = "No countries found." });
            }
            return Ok(response);
        }


        [HttpGet("ByName/{name}")]
        public async Task<IActionResult> GetCountryByName(string name)
        {
            var response = await _mediator.Send(new GetCountryByNameQuery { Name = name });
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody] AddCountryCommand command)
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

        //[HttpPost]
        //public async Task<IActionResult> AddCountry([FromBody] Country country)
        //{
        //    var result = await _countryService.AddCountryAsync(country);
        //    return Ok(result);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCountry(Guid id, [FromBody] Country country)
        //{
        //    if (id != country.Id)
        //        return BadRequest();

        //    await _countryService.UpdateCountryAsync(country);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCountry(Guid id)
        //{
        //    await _countryService.DeleteCountryAsync(id);
        //    return NoContent();
        //}
        //[HttpGet("/Countries/{id}")]
        //public async Task<IActionResult> GetCountryById(Guid id)
        //{
        //    var response = await _mediator.Send(new GetCountryByIdQuery(id));
        //    if (response == null)
        //    {
        //        return NotFound(new { message = "Country not found." });
        //    }
        //    return Ok(response);
        //}
        //[HttpGet("/Countries/Name/{name}")]
        //public async Task<IActionResult> GetCountryByName(string name)
        //{
        //    var response = await _mediator.Send(new GetCountryByNameQuery(name));
        //    if (response == null)
        //    {
        //        return NotFound(new { message = "Country not found." });
        //    }
        //    return Ok(response);
        //}
    }
}
