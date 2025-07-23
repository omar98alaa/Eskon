using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Core.Response;
using Eskon.Domian.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.Core.Features.PropertyFeatures.Commands.Command
{
    public record SetIsSuspendedPropertyCommand(Guid id, bool value, Guid ownerId) : IRequest<Response<string>>;
}
