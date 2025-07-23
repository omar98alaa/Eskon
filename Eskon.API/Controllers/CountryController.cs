using Eskon.API.Base;
using Eskon.Core.Features.Country_CityFeatures.Commands.Commands;
using Eskon.Core.Features.CountryFeatures.Queries.Models;
using Eskon.Domian.DTOs.Country_City;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/countries")]
        public async Task<IActionResult> GetAllCountries()
        {
            var result = await _mediator.Send(new GetCountryListQuery());
            return NewResult(result);
        }

        [HttpGet("/country/{name}")]
        public async Task<IActionResult> GetCountryByName(string name)
        {
            var result = await _mediator.Send(new GetCountryByNameQuery(name));
            return NewResult(result);
        }

        [HttpPost("/add/country")]
        public async Task<IActionResult> AddCountry([FromBody] AddCountryCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("/Edit/country/{id}")]
        public async Task<IActionResult> Edit(string name, [FromBody] CountryDTO dto)
        {
            var result = await _mediator.Send(new EditCountryCommand(name, dto));
            return Ok(result);
        }






    }
}
