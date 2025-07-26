using AutoMapper;
using Eskon.API.Base;
using Eskon.Core.Features.Country_CityFeatures.Commands.Commands;
using Eskon.Core.Features.CountryFeatures.Queries.Models;
using Eskon.Domian.DTOs.Country;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        private readonly IMapper _mapper;

        public CountryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var result = await Mediator.Send(new GetCountryListQuery());
            return NewResult(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCountryByName(string name)
        {
            var result = await Mediator.Send(new GetCountryByNameQuery(name));
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody] AddCountryCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] CountryUpdateDTO dto)
        {
            var result = await Mediator.Send(new EditCountryCommand(id, dto));
            return NewResult(result);
        }







    }
}
