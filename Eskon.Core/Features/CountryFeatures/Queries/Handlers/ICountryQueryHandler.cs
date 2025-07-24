using Eskon.Core.Features.CountryFeatures.Queries.Models;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country;
using Eskon.Domian.DTOs.Country_City;
using MediatR;

namespace Eskon.Core.Features.CountryFeatures.Queries.Handlers
{
    public interface ICountryQueryHandler : IRequestHandler<GetCountryByNameQuery, Response<CountryDTO>>,
                                            IRequestHandler<GetCountryListQuery, Response<List<CountryReadDTO>>>;
}
