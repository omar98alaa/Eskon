using Microsoft.EntityFrameworkCore;
using Eskon.Domian.Entities.Identity;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Eskon.Domian.Models;

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

        #endregion
    }
}
