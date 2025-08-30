using System.Linq.Expressions;

namespace ChatApp.Core.IDataService
{
    public interface IBaseDataService<TDataTransferObject> where TDataTransferObject : class
    {
        #region Public Methods

        Task<TDataTransferObject> AddAsync(TDataTransferObject entity);

        Task AddRangeAsync(IEnumerable<TDataTransferObject> entities);

        Task DeleteAsync(TDataTransferObject entity);
        Task DeleteByIdAsync(int id);
        Task DeleteRangeAsync(IEnumerable<TDataTransferObject> entity);

        Task<TDataTransferObject> FindAsync(int id);

        Task<IEnumerable<TDataTransferObject>> GetAllAsync();

        Task<IEnumerable<TResult>> GetSpecificPropertyAsync<TResult>(Expression<Func<TDataTransferObject, bool>> predicate, Expression<Func<TDataTransferObject, TResult>> selector);

        Task UpdateAsync(TDataTransferObject entity);

        Task UpdateOnlyPropertyAsync(TDataTransferObject entity, string propertyName);

        Task<IEnumerable<TDataTransferObject>> Where(Expression<Func<TDataTransferObject, bool>> predicate, params Expression<Func<TDataTransferObject, object>>[] includes);


        #endregion Public Methods
    }
}