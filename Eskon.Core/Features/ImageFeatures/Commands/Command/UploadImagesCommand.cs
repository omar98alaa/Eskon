using Eskon.Core.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.ImageFeatures.Commands.Command
{
    public record UploadImageCommand(IFormFile File) : IRequest<Response<string?>>;
}
