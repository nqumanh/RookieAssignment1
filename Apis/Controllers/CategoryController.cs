using Apis.Data;
using Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    [HttpGet("GetAllCategories")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        if (_context.Categories == null)
        {
            return NotFound();
        }

        var categories = await _context.Categories.ToListAsync();

        return Ok(categories);
    }

    // [HttpPost("AddCategory")]
    // public async Task<ActionResult<Category>> AddCategory(Category category)
    // {
    //     _context.Categories!.Add(category);
    //     await _context.SaveChangesAsync();

    //     return Ok();
    // }
}
