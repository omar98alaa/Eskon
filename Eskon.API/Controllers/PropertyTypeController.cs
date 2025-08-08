using Microsoft.AspNetCore.Mvc;
using Eskon.API.Base;
using AutoMapper;
using Eskon.Core.Features.PropertyTypeFeatures.Queries.Query;
namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyTypeController : BaseController
    {
        private readonly IMapper _mapper;

        public PropertyTypeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("GetAllPropertyTypes")]
        public async Task<IActionResult> GetAllPropertyTypes()
        {
            var result = await Mediator.Send(new GetAllPropertyTypesQuery());
            return NewResult(result);
        }

    }
}
