using AutoMapper;
using Eskon.Core.Features.CityFeatures.Queries.Models;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.CityDTOs;
using Eskon.Service.UnitOfWork;


namespace Eskon.Core.Features.CityFeatures.Queries.Handlers
{
    public class CityQueryHandler : ResponseHandler, ICItyQueryHandler
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

        public async Task<Response<List<CityReadDTO>>> Handle(GetCityListQuery request, CancellationToken cancellationToken)
        {
            var country = await _unitOfWork.CountryService.GetCountryByNameAsync(request.countryName);

            if(country == null)
            {
                return NotFound<List<CityReadDTO>>(message: "Country not found");
            }

            var cities = await _unitOfWork.CityService.GetAllCitiesPerCountryAsync(country);
            var citiesDto = _mapper.Map<List<CityReadDTO>>(cities);
            return Success(citiesDto);
        }
    }

}
