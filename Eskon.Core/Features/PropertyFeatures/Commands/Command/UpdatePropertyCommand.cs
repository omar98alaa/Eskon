using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Property;
using MediatR;

namespace Eskon.Core.Features.PropertyFeatures.Commands.Command
{
    public record UpdatePropertyCommand(Guid id,PropertyWriteDTO propertyWriteDTO, Guid ownerId) : IRequest<Response<PropertyDetailsDTO>>;
}
