using Eskon.Core.Response;
using Eskon.Domian.DTOs.City;
using MediatR;

namespace Eskon.Core.Features.CityFeatures.Queries.Models
{
    public record GetCityListQuery(string countryName) : IRequest<Response<List<CityReadDTO>>>;
}
