using AutoMapper;
using Eskon.Core.Features.Country_CityFeatures.Commands.Commands;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Domian.Models;
using Eskon.Service.Interfaces.Country_City;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Commands.Handler
{
    public class AddCountryHandler : IRequestHandler<AddCountryCommand, Response<CountryDTO>>
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public AddCountryHandler(
            ICountryService countryService,
            IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        public async Task<Response<CountryDTO>> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            var country = new Country { Name = request.Name };

            var addedCountry = await _countryService.AddCountryAsync(country);
            await _countryService.SaveChangesAsync();

            var result = _mapper.Map<CountryDTO>(addedCountry);
            return Response<CountryDTO>.Success(result, "Country added successfully");
        }
    }
}
