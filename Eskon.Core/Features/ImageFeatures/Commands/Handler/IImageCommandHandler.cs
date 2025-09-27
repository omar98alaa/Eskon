using Eskon.Core.Features.ImageFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Image;
using MediatR;

namespace Eskon.Core.Features.ImageFeatures.Commands.Handler
{
    public interface IImageCommandHandler : IRequestHandler<SaveImageCommand, Response<ImageReadDTO>>,
                                            IRequestHandler<DeleteImageCommand, Response<string>>;
}
