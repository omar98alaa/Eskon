using Microsoft.EntityFrameworkCore;
using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Eskon.Domian.Models;
using Eskon.Domain.Utilities;
using System.Linq.Expressions;
using System.Linq;

namespace Eskon.Infrastructure.Repositories
{
    public class FavouriteRepository : GenericRepositoryAsync<Favourite>, IFavouriteRepository
    {
        #region Fields
        private readonly DbSet<Favourite> _favouriteDbSet;
        #endregion

        #region Constructors
        public FavouriteRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _favouriteDbSet = myDbContext.Set<Favourite>();
        }
        #endregion

        #region Methods
        public async Task<Favourite?> GetFavouriteByIdAsync(Guid CustomerId, Guid PropertyId)
        {
            return await _favouriteDbSet.Include(f => f.Property).FirstOrDefaultAsync(f => f.UserId == CustomerId && f.PropertyId == PropertyId);
        }
        public async Task<Paginated<Favourite>> GetPaginatedFavouritesAsync(int pageNumber = 1, int itemsPerPage = 10, Expression<Func<Favourite, bool>>? filter = null)
        {
            IQueryable<Favourite> query = _myDbContext.Set<Favourite>().Include(f => f.Property);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            pageNumber = Math.Max(pageNumber, 1);
            itemsPerPage = Math.Max(itemsPerPage, 1);

            var data = await query
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            var total = await query.CountAsync();

            return new Paginated<Favourite>(data, pageNumber, itemsPerPage, total);
        }

        #endregion
    }
}
