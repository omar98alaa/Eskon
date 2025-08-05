using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Favourite;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Core.Features.FavouriteFeatures.Queries.Query
{
    public record GetUserFavouritesQuery(Guid CustomerId) : IRequest<Response<List<FavouriteReadDTO>>>;


}
