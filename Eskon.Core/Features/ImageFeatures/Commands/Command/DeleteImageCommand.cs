using Eskon.Core.Response;
using Eskon.Domian.DTOs.ImageDTO;
using MediatR;

namespace Eskon.Core.Features.ImageFeatures.Commands.Commands
{

    public record DeleteImageCommand(string FileName) : IRequest<Response<string>>;
}
