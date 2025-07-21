using Eskon.Core.Features.PropertyFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Property;
using MediatR;


namespace Eskon.Core.Features.PropertyFeatures.Queries.Handler
{
    public interface IPropertyQueryHandler : IRequestHandler<GetPropertyByIdQuery, Response<PropertyDetailsDTO>>,
                                             IRequestHandler<GetAssignedPendingPropertiesQuery, Response<Paginated<PropertySummaryDTO>>>,
                                             IRequestHandler<GetActivePropertiesPerOwnerQuery, Response<Paginated<PropertySummaryDTO>>>,
                                             IRequestHandler<GetPendingPropertiesPerOwnerQuery, Response<Paginated<PropertySummaryDTO>>>,
                                             IRequestHandler<GetSuspendedPropertiesPerOwnerQuery, Response<Paginated<PropertySummaryDTO>>>,
                                             IRequestHandler<GetRejectedPropertiesPerOwnerQuery, Response<Paginated<PropertySummaryDTO>>>;
}
