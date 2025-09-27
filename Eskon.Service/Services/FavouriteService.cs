using System.ComponentModel.DataAnnotations;
using Eskon.Domain.Utilities;
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
            return await _favouriteRepository.AddAsync(favourite);
        }

        public async Task<Paginated<Favourite>> GetPaginatedFavouritesPerCustomer(int pageNum, int itemsPerPage, Guid customerId)
        {
            return await _favouriteRepository.GetPaginatedAsync(pageNum, itemsPerPage, filter: f => f.UserId == customerId, includes: nameof(Favourite.Property));
        }

        public async Task RemoveFavouriteAsync(Favourite favourite)
        {
            await _favouriteRepository.DeleteAsync(favourite);
        }
        public async Task<Favourite?> GetFavouriteByIdAsync(Guid favouriteId)
        {
            return await _favouriteRepository.GetByIdAsync(favouriteId);
        }

        public async Task<Favourite?> GetFavouriteForUserAndPropertyAsync(Guid userId, Guid propertyId)
        {
            return (await _favouriteRepository.GetFilteredAsync(f => f.UserId == userId && f.PropertyId == propertyId)).SingleOrDefault();
        }
    }
}
