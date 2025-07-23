using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTO;
using MediatR;


namespace Eskon.Core.Features.CityFeatures.Queries.Models
{

    public record GetCityByNameQuery(string Name) : IRequest<Response<CityDTO>>;
}
