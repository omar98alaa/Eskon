using Eskon.Core.Features.PropertyTypeFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.PropertyType;
using MediatR;

namespace Eskon.Core.Features.PropertyTypeFeatures.Queries.Handler
{
    public interface IPropertyTypeQueryHandler : IRequestHandler<GetAllPropertyTypesQuery, Response<List<PropertyTypeDTO>>>;


}
