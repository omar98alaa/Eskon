using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eskon.Domian.Entities.Identity;
using Eskon.Domian.Models;
using Eskon.Infrastructure.Context;
using Eskon.Infrastructure.Generics;
using Eskon.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Eskon.Infrastructure.Repositories
{
    class PropertyRepository: GenericRepositoryAsync<Property>,IPropertyRepository
    {
        #region Fields
        private readonly DbSet<Property> _PropertyDbSet;
        #endregion

        #region Constructors
        public PropertyRepository(MyDbContext myDbContext) : base(myDbContext)
        {
            _PropertyDbSet = myDbContext.Set<Property>();
        }

        public async Task<List<Property>> GetPropertyByAdminIdAsync(Guid AdminId)
        {
            return await GetFiltered(x => x.AssignedAdminId == AdminId);
        }

        public async Task<List<Property>> getPropertybyCityAsync(string City, string Country)
        {
            return await GetFiltered(x=>x.City.Name==City&& x.City.Country.Name==Country);
        }

        public async Task<List<Property>> GetPropertyByOwnerIdAsync(Guid OwnerId)
        {
            return await GetFiltered(x => x.OwnerId == OwnerId);
        }

        public async Task<List<Property>> getPropertybyPriceRangAsync(int MinPrice, int MaxPrice)
        {
            return await GetFiltered(x => x.PricePerNight>=MinPrice&& x.PricePerNight<=MaxPrice);
        }

        public async Task<List<Property>> getPropertybyRatingAsync(int Rating)
        {
            return await GetFiltered(x => x.AverageRating == Rating);
        }

        public async Task SetAverageRatingAsync(Guid PropertyId)
        {
            Property property=await GetByIdAsync(PropertyId);
            property.AverageRating=property.Reviews.Average(x=>x.Rating);
        }


        #endregion
    }
}