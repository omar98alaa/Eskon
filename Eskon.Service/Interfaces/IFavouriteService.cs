using Eskon.Domain.Utilities;
using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IFavouriteService
    {
        #region Create
        public Task<Favourite> AddFavouriteAsync(Favourite favourite);
        #endregion

        #region Read
        public Task<Favourite?> GetFavouriteByIdAsync(Guid favouriteId);
        public Task<Favourite?> GetFavouriteForUserAndPropertyAsync(Guid userId, Guid propertyId);
        public Task<Paginated<Favourite>> GetPaginatedFavouritesPerCustomer(int pageNum, int itemsPerPage, Guid customerId);

        #endregion

        #region Delete
        public Task RemoveFavouriteAsync(Favourite favourite);
        #endregion

    }


}
