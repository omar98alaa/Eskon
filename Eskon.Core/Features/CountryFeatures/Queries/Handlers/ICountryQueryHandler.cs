using Eskon.Core.Features.CountryFeatures.Queries.Models;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.CountryDTOs;
using Eskon.Domian.DTOs.CountryDTOs;
using MediatR;

namespace Eskon.Core.Features.CountryFeatures.Queries.Handlers
{
    public interface ICountryQueryHandler : IRequestHandler<GetCountryByNameQuery, Response<CountryDTO>>,
                                            IRequestHandler<GetCountryListQuery, Response<List<CountryReadDTO>>>;
}
