using AutoMapper;
using Eskon.Core.Features.CityFeatures.Queries.Models;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTO;
using Eskon.Service.UnitOfWork;
using MediatR;


namespace Eskon.Core.Features.CityFeatures.Queries.Handlers
{
    public class CityQueryHandler : ResponseHandler,
     IRequestHandler<GetCityByNameQuery, Response<CityDTO>>,
     IRequestHandler<GetCityListQuery, Response<List<CityDTO>>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public CityQueryHandler(IMapper mapper, IServiceUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<Response<CityDTO>> Handle(GetCityByNameQuery request, CancellationToken cancellationToken)
        {
            var city = await _unitOfWork.CityService.GetCityByNameAsync(request.Name);
            if (city == null)
                return NotFound<CityDTO>("City not found");

            var cityDto = _mapper.Map<CityDTO>(city);
            return Success(cityDto);
        }

        public async Task<Response<List<CityDTO>>> Handle(GetCityListQuery request, CancellationToken cancellationToken)
        {
            var cities = await _unitOfWork.CityService.GetAllCitiesAsync();
            var citiesDto = _mapper.Map<List<CityDTO>>(cities);
            return Success(citiesDto);
        }
    }

}
