using Eskon.Core.Response;
using Eskon.Domian.DTOs.Country_City;
using Eskon.Domian.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Commands.Commands
{
    public class AddCityCommand : IRequest<Response<CityDTO>>
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
    }
}
