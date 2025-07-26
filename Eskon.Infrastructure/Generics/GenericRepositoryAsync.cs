
using Eskon.Domain.Utilities;
using Eskon.Domian.Entities;
using Eskon.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Eskon.Infrastructure.Generics
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class, IBaseModel
    {
        #region Fields
        protected readonly MyDbContext _myDbContext;
        #endregion

        #region Constructors
        public GenericRepositoryAsync(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        #endregion

        #region Actions
        public virtual async Task<T> AddAsync(T entity)
        {
            await _myDbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await _myDbContext.Set<T>().AddRangeAsync(entities);
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await _myDbContext.Set<T>().FindAsync(id);
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _myDbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _myDbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<Paginated<T>> GetPageAsync(int pageNumber = 1, int itemsPerPage = 10)
        {
            pageNumber = Math.Max(pageNumber, 1);
            itemsPerPage = Math.Max(itemsPerPage, 1);

            var data = await _myDbContext.Set<T>()
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            var total = await GetTotalCount();

            return new Paginated<T>(data, pageNumber, itemsPerPage, total);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _myDbContext.Set<T>().Update(entity);
        }

        public async Task UpdateRangeAsync(ICollection<T> entities)
        {
            _myDbContext.Set<T>().UpdateRange(entities);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _myDbContext.Set<T>().Remove(entity);
        }

        public virtual async Task SoftDeleteAsync(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            _myDbContext.Set<T>().Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _myDbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public virtual async Task SoftDeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.DeletedAt = DateTime.UtcNow;
                _myDbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _myDbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _myDbContext.Database.CommitTransaction();
        }

        public void RollBack()
        {
            _myDbContext.Database.RollbackTransaction();
        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _myDbContext.Set<T>().AsTracking().AsQueryable();
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return _myDbContext.Set<T>().AsNoTracking().AsQueryable();
        }

        public async Task<Paginated<T>> GetPaginatedAsync(int pageNumber = 1, int itemsPerPage = 10, Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _myDbContext.Set<T>();

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

            return new Paginated<T>(data, pageNumber, itemsPerPage, total);
        }

        public async Task<Paginated<T>> GetPaginatedSortedAsync<TKey>(Expression<Func<T, TKey>> sort, bool asc, int pageNumber = 1, int itemsPerPage = 10, Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _myDbContext.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (sort != null)
            {
                if (asc)
                {
                    query = query.OrderBy(sort);
                }
                else
                {
                    query = query.OrderByDescending(sort);
                }
            }

            pageNumber = Math.Max(pageNumber, 1);
            itemsPerPage = Math.Max(itemsPerPage, 1);

            var data = await query
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            var total = await query.CountAsync();

            return new Paginated<T>(data, pageNumber, itemsPerPage, total);
        }

        public async Task<int> GetTotalCount()
        {
            return await _myDbContext.Set<T>().CountAsync();
        }

        #endregion
    }
}
