using Eskon.Core.Response;
using Eskon.Domian.DTOs.CountryDTOs;
using MediatR;

namespace Eskon.Core.Features.CountryFeatures.Queries.Models
{
    public record GetCountryByNameQuery(string Name) : IRequest<Response<CountryDTO>>;
}
