using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTO;
using MediatR;

namespace Eskon.Core.Features.CityFeatures.Queries.Models
{
    public record GetCityListQuery : IRequest<Response<List<CityDTO>>>;
}
