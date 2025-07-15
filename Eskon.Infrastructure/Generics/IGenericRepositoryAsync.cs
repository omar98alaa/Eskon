using Eskon.Domian.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Eskon.Domian.Entities;

namespace Eskon.Infrastructure.Generics
{
    public interface IGenericRepositoryAsync<T> where T : class, IBaseModel
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task DeleteAsync(T entity);
        Task SoftDeleteAsync(T entity);
        Task DeleteRangeAsync(ICollection<T> entities);
        Task SoftDeleteRangeAsync(ICollection<T> entities);
        Task<int> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
    }
}
