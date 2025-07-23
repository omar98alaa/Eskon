using Eskon.API.Base;
using Eskon.Core.Features.CityFeatures.Commands.Commands;
using Eskon.Core.Features.CityFeatures.Queries.Models;
using Eskon.Domian.DTOs.CityDTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/cities")]
        public async Task<IActionResult> GetAllCities()
        {
            var result = await _mediator.Send(new GetCityListQuery());
            return NewResult(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCityByName(string name)
        {
            var result = await _mediator.Send(new GetCityByNameQuery(name));
            return NewResult(result);
        }

        [HttpPost("/add/city")]
        [Authorize("Admin")]
        public async Task<IActionResult> AddCity([FromBody] AddCityCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("/edit/city/{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] CityUpdateDTO dto)
        {
            var result = await _mediator.Send(new EditCityCommand(id, dto));
            return NewResult(result);
        }


        [HttpDelete("delete/city/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCityCommand(id));
            return NewResult(result);
        }



    }
}
