using Apis.Data;
using Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedViewModels;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly BookStoreContext _context;
    public CategoryController(BookStoreContext context)
    {
        _context = context;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
    {
        return await _context.Categories!
                        .Select(x => CategoryDTO(x))
                        .ToListAsync();
    }

    private static CategoryDTO CategoryDTO(Category category) =>
        new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        };
}
