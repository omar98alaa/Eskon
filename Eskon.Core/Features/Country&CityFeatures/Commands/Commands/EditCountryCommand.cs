using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Commands.Commands
{
    public class EditCountryCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public List<string> CityNames { get; set; } = new List<string>();
    }
}
