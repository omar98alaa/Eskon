using Eskon.API.Base;
using Eskon.Core.Features.Dashboard.Queries.Query;
using Eskon.Domian.DTOs.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Root")]

    public class DashboardController : BaseController
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]

        public async Task<ActionResult<DashboardDto>> GetDashboard()
        {
            var result = await _mediator.Send(new GetDashboardStateQuery());
            return Ok(result);
        }
    }
}

