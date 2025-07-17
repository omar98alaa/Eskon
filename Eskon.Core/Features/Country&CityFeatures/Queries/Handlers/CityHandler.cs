using AutoMapper;
using Eskon.Core.Features.Country_CityFeatures.Queries.Models;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces.Country_City;
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
    public class CityHandler : IRequestHandler<GetCityListQuery, List<CityDTO>>
    {
        #region Fields
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CityHandler(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }
        #endregion

        #region Handles Functions

        public async Task<List<CityDTO>> Handle(GetCityListQuery request, CancellationToken cancellationToken)
        {
            var cities = await _cityService.GetAllCitiesAsync();

            return cities.Select(c => new CityDTO
            {
                Name = c.Name,
                CountryName = c.Country?.Name 
            }).ToList();
        }



        #endregion




        //async Task<List<City>> IRequestHandler<GetCityListQuery, List<City>>.Handle(GetCityListQuery request, CancellationToken cancellationToken)
        //{
        //    var cities = await _cityService.GetAllCitiesAsync();
        //    return _mapper.Map<List<CityDTO>>(cities);
        //}

    }
}
