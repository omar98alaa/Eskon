using Eskon.Core.Response;
using Eskon.Domian.DTOs.Image;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Eskon.Core.Features.ImageFeatures.Commands.Command
{
    public record SaveImageCommand(IFormFile image) : IRequest<Response<ImageReadDTO>>;
}
