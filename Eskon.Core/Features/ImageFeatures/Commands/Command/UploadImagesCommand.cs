using Eskon.Core.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Eskon.Core.Features.ImageFeatures.Commands.Command
{
    public record UploadImageCommand(IFormFile File) : IRequest<Response<string?>>;
}
