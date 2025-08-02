using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.DTOs.Property;
using Eskon.Core.Response;
using MediatR;


namespace Eskon.Core.Features.PropertyFeatures.Commands.Command
{
        public record AddPropertyCommand(Guid ownerId,PropertyWriteDTO PropertyWriteDTO) : IRequest<Response<PropertyDetailsDTO>>;
}
