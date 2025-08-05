using Eskon.Core.Features.FavouriteFeatures.Commands.Command;
using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.FavouriteFeatures.Commands.Handler
{
    public interface IFavouriteCommandHandler : IRequestHandler<AddFavouriteCommand, Response<string>>,
                                                IRequestHandler<RemoveFavouriteCommand, Response<string>>;


}
