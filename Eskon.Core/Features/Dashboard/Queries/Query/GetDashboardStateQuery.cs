
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Dashboard;
using MediatR;

namespace Eskon.Core.Features.Dashboard.Queries.Query
{
    public record GetDashboardStateQuery : IRequest<Response<DashboardDto>>;
}
