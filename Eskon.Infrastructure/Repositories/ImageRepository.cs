
using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Eskon.Infrastructure.Repositories
{
    public class ImageRepository : GenericRepositoryAsync<Image>, IImageRepository
    {
        #region Fields
        private readonly DbSet<Image> _imageDbSet;
        #endregion

        #region constructor
        public ImageRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _imageDbSet = myDbContext.Set<Image>();
        }
        #endregion

   

    }
}
