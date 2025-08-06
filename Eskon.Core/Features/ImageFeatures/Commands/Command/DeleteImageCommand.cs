using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.ImageFeatures.Commands.Command
{
    public record DeleteImageCommand(string imageName) : IRequest<Response<string>>;
}
