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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Commands.Handler
{
    public class AddCityHandler : IRequestHandler<AddCityCommand, Response<CityDTO>>
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public AddCityHandler(
            ICityService cityService,
            ICountryService countryService,
            IMapper mapper)
        {
            _cityService = cityService;
            _countryService = countryService;
            _mapper = mapper;
        }

        public async Task<Response<CityDTO>> Handle(AddCityCommand request, CancellationToken cancellationToken)
        {
            var country = await _countryService.GetCountryByNameAsync(request.CountryName);

            if (country == null)
            {
                return Response<CityDTO>.Fail($"Country '{request.CountryName}' not found");
            }

           
            var city = new City
            {
                Name = request.CityName,
                CountryId = country.Id 
            };

         
            var addedCity = await _cityService.AddCityAsync(city);
            await _cityService.SaveChangesAsync();

           
            var result = _mapper.Map<CityDTO>(addedCity);
            return Response<CityDTO>.Success(result, "City added successfully");
        }
    }
}
