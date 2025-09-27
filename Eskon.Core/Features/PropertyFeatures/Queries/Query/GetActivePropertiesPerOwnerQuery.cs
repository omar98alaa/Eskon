using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Property;
using MediatR;

namespace Eskon.Core.Features.PropertyFeatures.Queries.Query
{
    public record GetActivePropertiesPerOwnerQuery(Guid ownerId, int pageNum, int itemsPerPage) : IRequest<Response<Paginated<PropertySummaryDTO>>>;
}
