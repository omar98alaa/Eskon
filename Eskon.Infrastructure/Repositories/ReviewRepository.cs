using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Infrastructure.Repositories
{
    class ReviewRepository : GenericRepositoryAsync<Review>,IReviewRepository
    {

        #region Fields
        private readonly DbSet<Review> _reviewDbSet;
        #endregion

        #region Constructors
        public ReviewReposatory(MyDbContext myDbContext) : base(myDbContext)
        {
            _reviewDbSet = myDbContext.Set<Review>();
        }

        #endregion

        #region Methods

        

        #endregion


    }
}
