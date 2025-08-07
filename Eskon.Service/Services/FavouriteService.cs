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
            await _favouriteRepository.AddAsync(favourite);
            return favourite;
        }

        public async Task<Paginated<Favourite>> GetPaginatedFavouritesPerCustomer(int pageNum, int itemsPerPage,Guid customerId)
        {
            return await _favouriteRepository.GetPaginatedFavouritesAsync(pageNum,itemsPerPage ,filter: f => f.UserId == customerId);
        }

        public async Task RemoveFavouriteAsync(Favourite favourite)
        {
            await _favouriteRepository.DeleteAsync(favourite);
        }
        public async Task<Favourite?> GetFavouriteByIdAsync(Guid CustomerId, Guid PropertyId)
        {
            return await _favouriteRepository.GetFavouriteByIdAsync(CustomerId, PropertyId);
        }
    }
}
