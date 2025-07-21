using Eskon.Core.Response;
using Eskon.Domian.DTOs.Property;
using MediatR;

namespace Eskon.Core.Features.PropertyFeatures.Queries.Query
{
    public record GetPropertyByIdQuery(Guid propertyId) : IRequest<Response<PropertyDetailsDTO>>;
}
