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


    public class EditCountryHandler : IRequestHandler<EditCountryCommand, bool>
    {
        private readonly ICountryService _countryService;

        public EditCountryHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<bool> Handle(EditCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _countryService.GetCountryByNameAsync(request.Name);
            if (country == null) return false;

            country.Name = request.Name;
            await _countryService.UpdateCountryAsync(country);
            return true;
        }

    }
}
