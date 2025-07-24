using AutoMapper;
using Eskon.Core.Features.CountryFeatures.Queries.Models;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Service.UnitOfWork;
using MediatR;


namespace Eskon.Core.Features.CountryFeatures.Queries.Handlers
{
    public class CountryQueryHandler : ResponseHandler, IRequestHandler<GetCountryByNameQuery, Response<CountryDTO>>,
        IRequestHandler<GetCountryListQuery, Response<List<CountryDTO>>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IServiceUnitOfWork _unitOfWork;

        #endregion

        public CountryQueryHandler(IMapper mapper, IServiceUnitOfWork unitOfWork)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<CountryDTO>> Handle(GetCountryByNameQuery request, CancellationToken cancellationToken)
        {
            var country = await _unitOfWork.CountryService.GetCountryByNameAsync(request.Name);

            if (country == null)
                return NotFound<CountryDTO>("Country not found");

            var dto = _mapper.Map<CountryDTO>(country);
            return Success(dto);
        }

        public async Task<Response<List<CountryDTO>>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
        {
            var countries = await _unitOfWork.CountryService.GetCountryListAsync();
            var dtoList = _mapper.Map<List<CountryDTO>>(countries);
            return Success(dtoList);
        }
    }
}
