using Eskon.Core.Response;
using Eskon.Domian.Entities;
using MediatR;

namespace Eskon.Core.Features.EscrowTransactionFeatures.Queries.Query
{
    public record GetAllPendingEscrowReleasesQuery() : IRequest<Response<List<EscrowTransaction>>>;

}
