using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Core.Features.PropertyFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Property;
using MediatR;

namespace Eskon.Core.Features.PropertyFeatures.Commands.Handler
{
    interface IPropertyCommandHandler: IRequestHandler<AddPropertyCommand, Response<PropertyDetailsDTO>>,
        IRequestHandler<SetPropertyAsAcceptedCommand, Response<string>>, IRequestHandler<SetPropertyAsRejectedCommand, Response<string>>, IRequestHandler<SetIsSuspendedPropertyCommand, Response<string>>,
        IRequestHandler<UpdatePropertyCommand, Response<PropertyDetailsDTO>>, IRequestHandler<DeletePropertyCommand, Response<PropertyDetailsDTO>>
    {

    }
}
