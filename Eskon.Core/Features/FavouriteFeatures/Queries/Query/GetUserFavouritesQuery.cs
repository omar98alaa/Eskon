using Eskon.Core.Response;
using Eskon.Domain.Utilities;
using Eskon.Domian.DTOs.Favourite;
using MediatR;
namespace Eskon.Core.Features.FavouriteFeatures.Queries.Query
{
    public record GetUserFavouritesQuery(int pageNumber, int itemsPerPage,Guid CustomerId) : IRequest<Response<Paginated<FavouriteReadDTO>>>;


}
