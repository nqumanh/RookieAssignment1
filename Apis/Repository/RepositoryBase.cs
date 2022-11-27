using System.Linq.Expressions;
using Apis.Data;
using Apis.Interface;
using Microsoft.EntityFrameworkCore;

namespace Apis.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected BookStoreContext _context { get; set; }

    public RepositoryBase(BookStoreContext context)
    {
        _context = context;
    }
    public IQueryable<T> FindAll() => _context.Set<T>().AsNoTracking();
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        _context.Set<T>().Where(expression).AsNoTracking();
    public void Create(T entity) => _context.Set<T>().Add(entity);
    public void Update(T entity) => _context.Set<T>().Update(entity);
    public void Delete(T entity) => _context.Set<T>().Remove(entity);
}