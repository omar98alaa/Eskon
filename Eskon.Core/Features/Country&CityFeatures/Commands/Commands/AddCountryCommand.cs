using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country_City;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Commands.Commands
{
    public class AddCountryCommand : IRequest<Response<CountryDTO>>
    {
        public string Name { get; set; }
    }
}
