using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eskon.Service.Interfaces
{
   public interface IReviewService
    {
        public Task<List<Review>> GetPropertyReviewsAsync(Guid propId);

        public Task<Review> CreatePropertyReviewAsync(Review review);

        public Task UpdatePropertyReviewAsync(Review review); 

       public Task DeletePropertyReviewAsync(Review review);


        public Task<int> SaveChangesAsync();
    }


}
