using AutoMapper;
using Eskon.Core.Features.Country_CityFeatures.Queries.Models;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Service.Interfaces.Country_City;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Queries.Handlers
{
    internal class CountryByNameHandler : IRequestHandler<GetCountryByNameQuery, CountryDTO>
    {
        #region Fields
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        public CountryByNameHandler(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }
        #endregion


        public async Task<CountryDTO> Handle(GetCountryByNameQuery request, CancellationToken cancellationToken)
        {
            var country = await _countryService.GetCountryByNameAsync(request.Name);
            return _mapper.Map<CountryDTO>(country);
        }

    }
}
