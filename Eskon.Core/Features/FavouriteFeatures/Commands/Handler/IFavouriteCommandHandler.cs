using Eskon.Core.Features.FavouriteFeatures.Commands.Command;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Favourite;
using MediatR;

namespace Eskon.Core.Features.FavouriteFeatures.Commands.Handler
{
    public interface IFavouriteCommandHandler : IRequestHandler<AddFavouriteCommand, Response<FavouriteReadDTO>>,
                                                IRequestHandler<RemoveFavouriteCommand, Response<string>>;


}
