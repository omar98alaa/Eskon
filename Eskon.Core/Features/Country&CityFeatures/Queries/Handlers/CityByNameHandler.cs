using AutoMapper;
using Eskon.Core.Features.Country_CityFeatures.Queries.Models;
using Eskon.Domian.DTOs.Country_City;
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
    public class CityByNameHandler : IRequestHandler<GetCityByNameQuery, CityDTO>
    {
        #region Fields
       private readonly ICityService _cityService;
       private readonly IMapper _mapper;

        #endregion

    #region Constructor
    public CityByNameHandler(ICityService cityService, IMapper mapper)
    {
        _cityService = cityService;
        _mapper = mapper;
    }
    #endregion


    public async Task<CityDTO> Handle(GetCityByNameQuery request, CancellationToken cancellationToken)
        {
            var city = await _cityService.GetCityByNameAsync(request.Name);
            return _mapper.Map<CityDTO>(city);
        }

     
    }
}
