using Apis.Data;
using Apis.Models;
using Microsoft.EntityFrameworkCore;

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
        return await _context.Categories!
                .ToListAsync();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}