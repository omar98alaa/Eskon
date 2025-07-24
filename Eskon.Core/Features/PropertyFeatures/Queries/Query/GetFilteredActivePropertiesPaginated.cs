using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Property;
using Eskon.Domian.Utilities;
using MediatR;

namespace Eskon.Core.Features.PropertyFeatures.Queries.Query
{
    public record GetFilteredActivePropertiesPaginated(int pageNum, int itemsPerPage, PropertySearchFilters propertySearchFilters) : IRequest<Response<Paginated<PropertySummaryDTO>>>;
}
