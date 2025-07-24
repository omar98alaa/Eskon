using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country_City;
using MediatR;

namespace Eskon.Core.Features.CountryFeatures.Queries.Models
{

    public record GetCountryListQuery() : IRequest<Response<List<CountryDTO>>>;

}
