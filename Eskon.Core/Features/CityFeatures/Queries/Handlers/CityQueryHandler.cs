using AutoMapper;
using Eskon.Core.Features.CityFeatures.Queries.Models;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTO;
using Eskon.Service.Interfaces;
using MediatR;


namespace Eskon.Core.Features.CityFeatures.Queries.Handlers
{
    public class CityQueryHandler : ResponseHandler,
     IRequestHandler<GetCityByNameQuery, Response<CityDTO>>,
     IRequestHandler<GetCityListQuery, Response<List<CityDTO>>>
    {
        #region Fields
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CityQueryHandler(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }
        #endregion

        public async Task<Response<CityDTO>> Handle(GetCityByNameQuery request, CancellationToken cancellationToken)
        {
            var city = await _cityService.GetCityByNameAsync(request.Name);
            if (city == null)
                return NotFound<CityDTO>("City not found");

            var cityDto = _mapper.Map<CityDTO>(city);
            return Success(cityDto);
        }

        public async Task<Response<List<CityDTO>>> Handle(GetCityListQuery request, CancellationToken cancellationToken)
        {
            var cities = await _cityService.GetAllCitiesAsync();
            var citiesDto = _mapper.Map<List<CityDTO>>(cities);
            return Success(citiesDto);
        }
    }

}
