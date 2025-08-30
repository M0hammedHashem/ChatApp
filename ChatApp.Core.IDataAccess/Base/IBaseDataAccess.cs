using System.Linq.Expressions;

namespace ChatApp.Core.IDataAccess
{
    public interface IBaseDataAccess<TEntity> : IDisposable where TEntity : class
    {
        #region Public Methods

        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        Task DeleteRangeAsync(IEnumerable<TEntity> entity);

        Task<TEntity> FindAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TResult>> GetSpecificPropertyAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);

        Task UpdateAsync(TEntity entity);

        Task UpdateOnlyPropertyAsync(TEntity entity, string propertyName);

        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        #endregion Public Methods
    }
}