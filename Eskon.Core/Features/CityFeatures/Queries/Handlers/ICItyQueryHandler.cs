using Eskon.Core.Features.CityFeatures.Queries.Models;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTOs;
using MediatR;

namespace Eskon.Core.Features.CityFeatures.Queries.Handlers
{
    public interface ICItyQueryHandler : IRequestHandler<GetCityByNameQuery, Response<CityDTO>>,
                                         IRequestHandler<GetCityListQuery, Response<List<CityReadDTO>>>;
}
