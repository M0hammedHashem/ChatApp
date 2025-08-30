using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChatApp.Core.DataAccess
{
    public abstract class BaseDataAccess<TEntity> : IBaseDataAccess<TEntity> where TEntity : class
    {
        private readonly IChatApp_DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseDataAccess(IChatApp_DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Marks an entity to be added and returns it. The actual save is handled by the Unit of Work.
        /// </summary>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }


        /// <summary>
        /// Marks a range of entities to be added. The actual save is handled by the Unit of Work.
        /// </summary>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {

            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();


        }

        /// <summary>
        /// Marks an entity for deletion. The change is saved later by the Unit of Work.
        /// </summary>
        public Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Finds an entity by its ID and marks it for deletion.
        /// </summary>
        public async Task DeleteByIdAsync(int id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);
            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
            }
        }

        /// <summary>
        /// Marks a range of entities for deletion.
        /// </summary>
        public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetSpecificPropertyAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector)
        {
            return await _dbSet.AsNoTracking().Where(predicate).Select(selector).ToListAsync();
        }

        /// <summary>
        /// Marks an entity as modified.
        /// </summary>
        public Task UpdateAsync(TEntity entity)
        {
            // This tells the DbContext to start tracking the entity and mark it as modified.
            // It correctly handles both new and already-tracked entities.
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
        public Task UpdateOnlyPropertyAsync(TEntity entity, string propertyName)
        {
            _dbContext.Entry(entity).Property(propertyName).IsModified = true;
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return await IncludeProperties(_dbSet.AsNoTracking(), predicate, includes).ToListAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        private IQueryable<TEntity> IncludeProperties(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }
    }
}
