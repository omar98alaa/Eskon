using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.CityFeatures.Commands.Commands;
using Eskon.Core.Features.CityFeatures.Queries.Models;
using Eskon.Domian.DTOs.CityDTO;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        private readonly IMapper _mapper;

        public CityController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("/cities")]
        public async Task<IActionResult> GetAllCities()
        {
            var result = await Mediator.Send(new GetCityListQuery());
            return NewResult(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCityByName(string name)
        {
            var result = await Mediator.Send(new GetCityByNameQuery(name));
            return NewResult(result);
        }

        [HttpPost("/add/city")]
        //[Authorize("Admin")]
        public async Task<IActionResult> AddCity([FromBody] CityDTO city)
        {
            var result = await Mediator.Send(new AddCityCommand(city));
            return NewResult(result);
        }

        [HttpPut("/edit/city/{name}")]
        public async Task<IActionResult> Edit(string name, [FromBody] CityUpdateDTO dto)
        {
            var result = await Mediator.Send(new EditCityCommand(name, dto));
            return NewResult(result);
        }



        [HttpDelete("delete/city/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteCityCommand(id));
            return NewResult(result);
        }



    }
}
