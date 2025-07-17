using Eskon.Core.Features.Country_CityFeatures.Commands.Commands;
using Eskon.Service.Interfaces.Country_City;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Commands.Handler
{
    public class EditCityHandler : IRequestHandler<EditCityCommand, bool>
    {
        private readonly ICityService _cityService;

        public EditCityHandler(ICityService cityService)
        {
            _cityService = cityService;
        }

        public async Task<bool> Handle(EditCityCommand request, CancellationToken cancellationToken)
        {
            var city = await _cityService.GetCityByNameAsync(request.Name);
            if (city == null)
                throw new KeyNotFoundException("City not found.");

            city.Name = request.Name;
            city.CountryId = request.CountryId;

            await _cityService.UpdateCityAsync(city);
            return true;
        }
    }
}
