using System.Linq.Expressions;
using Apis.Data;
using Apis.Interface;
using Microsoft.EntityFrameworkCore;

namespace Apis.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BookStoreContext _context;
    internal DbSet<T> _dbSet;

    public GenericRepository(BookStoreContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual IQueryable<T> GetWithCondition(
        Expression<Func<T, bool>>? filter,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        string includeProperties = "")
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query);
        }
        else
        {
            return query;
        }
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public virtual async Task<T?> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }
    public async Task<T> UpdateAsync(T entity)
    {
        _context.Entry(entity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<T> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
