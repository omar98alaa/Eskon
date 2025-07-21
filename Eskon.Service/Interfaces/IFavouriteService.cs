using Eskon.Domian.Models;

namespace Eskon.Service.Interfaces
{
    public interface IFavouriteService
    {
        #region Create
        public Task<Favourite> AddFavouriteAsync(Favourite favourite);
        #endregion

        #region Read
        public Task<List<Favourite>> GetFavouritesPerCustomer(Guid customerId);

        #endregion

        #region Delete
        public Task RemoveFavouriteAsync(Favourite favourite);
        #endregion

    }


}
