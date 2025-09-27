using Eskon.Core.Response;
using Eskon.Domian.DTOs.PropertyType;
using MediatR;

namespace Eskon.Core.Features.PropertyTypeFeatures.Queries.Query
{
    public record GetAllPropertyTypesQuery() : IRequest<Response<List<PropertyTypeDTO>>>;

}
