using System.ComponentModel.DataAnnotations;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Interfaces;
using Eskon.Service.Interfaces;

namespace Eskon.Service.Services
{
    public class FavouriteService : IFavouriteService
    {

        #region Fields
        private readonly IFavouriteRepository _favouriteRepository;
        #endregion

        #region Constructors
        public FavouriteService(IFavouriteRepository favouriteRepository)
        {
            _favouriteRepository = favouriteRepository;
        }
        #endregion

        public async Task<Favourite> AddFavouriteAsync(Favourite favourite)
        {
            await _favouriteRepository.AddAsync(favourite);
            return favourite;
        }

        public async Task<List<Favourite>> GetFavouritesPerCustomer(Guid customerId)
        {
            return await _favouriteRepository.GetFilteredAsync(f => f.UserId == customerId);
        }

        public async Task RemoveFavouriteAsync(Favourite favourite)
        {
            await _favouriteRepository.DeleteAsync(favourite);
        }
    }
}
