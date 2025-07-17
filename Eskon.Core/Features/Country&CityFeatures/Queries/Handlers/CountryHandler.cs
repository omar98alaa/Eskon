using AutoMapper;
using Eskon.Core.Features.Country_CityFeatures.Queries.Models;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Domian.Models;
using Eskon.Service.Interfaces.Country_City;
using Eskon.Service.Services.Country_City;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Queries.Handlers
{
    public class CountryHandler : IRequestHandler<GetCountryListQuery, List<CountryDTO>>
    {
        #region Fields
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public CountryHandler(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;

        }
        #endregion

        #region Handle Functions

        public async Task<List<CountryDTO>> Handle(GetCountryListQuery request, CancellationToken cancellationToken)
        {
            var countries = await _countryService.GetCountryListAsync();

            return countries.Select(c => new CountryDTO
            {
                Name = c.Name,
                Cities = c.Cities?.Select(city => new CityDTO
                {
                    Name = city.Name
                }).ToList()
            }).ToList();
        }



        #endregion


    }
    }

