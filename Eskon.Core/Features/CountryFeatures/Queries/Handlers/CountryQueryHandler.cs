using AutoMapper;
using Eskon.Core.Features.Country_CityFeatures.Commands.Commands;
using Eskon.Core.Features.CountryFeatures.Queries.Models;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Service.Interfaces;
using MediatR;


namespace Eskon.Core.Features.CountryFeatures.Queries.Handlers
{
    public class CountryQueryHandler : ResponseHandler, IRequestHandler<GetCountryByNameQuery, Response<CountryDTO>>,
        IRequestHandler<GetCountryListQuery, Response<List<CountryDTO>>>
    {
        #region Fields
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        #endregion

        public CountryQueryHandler(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }
        public async Task<Response<CountryDTO>> Handle(GetCountryByNameQuery request, CancellationToken cancellationToken)
        {
            var country = await _countryService.GetCountryByNameAsync(request.Name);

            if (country == null)
                return Response<CountryDTO>.Fail("Country not found");

            var dto = _mapper.Map<CountryDTO>(country);
            return Response<CountryDTO>.Success(dto);
        }

        public async Task<Response<List<CountryDTO>>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
        {
            var countries = await _countryService.GetCountryListAsync();
            var dtoList = _mapper.Map<List<CountryDTO>>(countries);
            return Response<List<CountryDTO>>.Success(dtoList);
        }
    }
}
