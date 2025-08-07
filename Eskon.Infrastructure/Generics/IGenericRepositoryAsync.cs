using Eskon.Domain.Utilities;
using Eskon.Domian.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Eskon.Infrastructure.Generics
{
    public interface IGenericRepositoryAsync<T> where T : class, IBaseModel
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>>? filter = null, string? includes = null);
        Task<Paginated<T>> GetPageAsync(int pageNumber = 1, int ItemsPerPage = 10);
        Task<Paginated<T>> GetPaginatedAsync(int pageNumber = 1, int ItemsPerPage = 10, Expression<Func<T, bool>>? filter = null, string? includes = null);
        Task<Paginated<T>> GetPaginatedSortedAsync<TKey>(Expression<Func<T, TKey>> sort, bool asc, int pageNumber = 1, int itemsPerPage = 10, Expression<Func<T, bool>>? filter = null, string? includes = null);
        Task<int> GetTotalCount();
        Task<T> GetByIdAsync(object id);
        Task DeleteAsync(T entity);
        Task SoftDeleteAsync(T entity);
        Task DeleteRangeAsync(ICollection<T> entities);
        Task SoftDeleteRangeAsync(ICollection<T> entities);
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
    }
}
