using Eskon.Core.Features.FavouriteFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Favourite;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.FavouriteFeatures.Queries.Handler
{
    public interface IFavouriteQueryHandler : IRequestHandler<GetUserFavouritesQuery, Response<Paginated<FavouriteReadDTO>>>;

}
