using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Commands.Commands
{
    public class EditCityCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
