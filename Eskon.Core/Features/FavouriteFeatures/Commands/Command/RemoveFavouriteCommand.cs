using Eskon.Core.Response;
using MediatR;

namespace Eskon.Core.Features.FavouriteFeatures.Commands.Command
{
    public record RemoveFavouriteCommand(Guid UserId, Guid favouriteId) : IRequest<Response<string>>;

}
