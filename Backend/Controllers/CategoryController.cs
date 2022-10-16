using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Controllers;

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
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _context.Categories!.ToListAsync();
        if (result == null) return BadRequest();
        return Ok(result);
    }

    [HttpPost("AddCategory")]
    public async Task<IActionResult> AddCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok();
    }


}
