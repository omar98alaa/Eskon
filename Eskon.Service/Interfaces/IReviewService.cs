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

        Task<Review> CreatePropertyReviewAsync(Review review);

        Task UpdatePropertyReviewAsync(Review review); 

        Task DeletePropertyReviewAsync(Review review);


        Task<int> SaveChangesAsync();
    }


}
