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

    [HttpGet("GetProductsByCategory/{id}")]
    public async Task<IActionResult> GetCategoriesByProductId(long id)
    {
        var products = _context.Products.Where(product => product.Categories.Any(category => category.Id == id));
        if (products == null) return Ok(new List<Product>());
        return Ok(products);
    }

    [HttpPost("GetProductsByCategoryName")]
    public async Task<IActionResult> GetProductsByCategoryName([FromBody]string categoryName)
    {
        var result = await _context.Categories.Where(category => category.Name == categoryName).FirstOrDefaultAsync();
        if (result == null) return NotFound();
        var products = _context.Products.Where(product => product.Categories.Any(category => category.Id == result.Id));
        return Ok(products);
    }
}
