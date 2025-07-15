
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Eskon.Infrastructure.Context;

namespace Eskon.Infrastructure.Generics
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
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

        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _myDbContext.Entry(entity).State = EntityState.Deleted;
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

        public async Task<int> SaveChangesAsync()
        {
            return await _myDbContext.SaveChangesAsync();
        }

        #endregion
    }
}
