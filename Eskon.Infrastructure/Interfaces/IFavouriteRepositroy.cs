using Eskon.Domain.Utilities;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Generics;
using System.Linq.Expressions;

namespace Eskon.Infrastructure.Interfaces
{
    public interface IFavouriteRepository : IGenericRepositoryAsync<Favourite>
    {
        public Task<Favourite?> GetFavouriteByIdAsync(Guid CustomerId, Guid PropertyId);
        public Task<Paginated<Favourite>> GetPaginatedFavouritesAsync(int pageNumber = 1, int itemsPerPage = 10, Expression<Func<Favourite, bool>>? filter = null);


    }
}
