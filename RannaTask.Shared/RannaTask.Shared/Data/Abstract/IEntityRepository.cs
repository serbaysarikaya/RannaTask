using RannaTask.Shared.Entities.Abstract;
using System.Linq.Expressions;

namespace RannaTask.Shared.Data.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includdeProperties);
        Task<IList<T>> GelAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includdeProperties);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
