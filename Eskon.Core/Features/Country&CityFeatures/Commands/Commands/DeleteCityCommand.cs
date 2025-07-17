using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.Country_CityFeatures.Commands.Commands
{
    public class DeleteCityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
