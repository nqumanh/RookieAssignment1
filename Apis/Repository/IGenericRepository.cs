using System.Linq.Expressions;

namespace Apis.Repository;

public interface IGenericRepository<T>
{
    IQueryable<T> Entities { get; }
    IQueryable<T> GetWithCondition(
        Expression<Func<T, bool>>? filter,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        string includeProperties = "");
    Task<T> AddAsync(T entity);
    Task<T?> GetByIdAsync(object id);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}