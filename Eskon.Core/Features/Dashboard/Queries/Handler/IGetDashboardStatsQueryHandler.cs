
using Eskon.Core.Features.Dashboard.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Dashboard;
using MediatR;

namespace Eskon.Core.Features.Dashboard.Queries.Handler
{
    public interface IGetDashboardStatsQueryHandler
        : IRequestHandler<GetDashboardStateQuery, Response<DashboardDto>>
    {
    }
}
