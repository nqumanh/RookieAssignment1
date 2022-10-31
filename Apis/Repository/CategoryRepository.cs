using Apis.Data;
using Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedViewModels;

namespace Apis.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly BookStoreContext _context;
    public CategoryRepository(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAll()
    {
        return await _context.Categories!.ToListAsync();
    }

    public void Create(Category category)
    {
        _context.Categories?.Add(category);
    }

    public async Task<Category?> Get(int id)
    {
        return await _context.Categories!.FindAsync(id);
    }

    public void Update(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
    }

    public async void Delete(int id)
    {
        var category = await _context.Categories!.FindAsync(id);
        _context.Categories.Remove(category!);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public bool IsExisted(int id)
    {
        return _context.Categories!.Any(e => e.Id == id);
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}