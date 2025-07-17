using Eskon.Domian.DTOs.Country_City;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Queries.Models
{
    public class GetCountryByNameQuery : IRequest<CountryDTO>
    {
        public string Name { get; set; }
    }
}
