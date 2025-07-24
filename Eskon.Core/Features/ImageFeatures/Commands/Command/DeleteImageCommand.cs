using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.ImageFeatures.Commands.Commands
{

    public record DeleteImageCommand(string FileName) : IRequest<Response<string>>;
}
