using Eskon.Core.Response;
using Eskon.Domian.DTOs.Favourite;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.FavouriteFeatures.Commands.Command
{
    public record RemoveFavouriteCommand(Guid UserId, Guid PropertyId) : IRequest<Response<string>>;

}
