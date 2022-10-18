using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly BookStoreContext _context;

    public ProductController(BookStoreContext context)
    {
        _context = context;
    }


    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await _context.Products!.ToListAsync();
        if (result == null) return BadRequest();
        return Ok(result);
    }
    [HttpPut("EditCategoriesOfProduct/{id}")]
    public async Task<IActionResult> EditCategoriesOfProduct(long id, long categoryId)
    {
        var product = await _context.Products.FindAsync(id);
        var category = await _context.Categories.FindAsync(categoryId);
        if (product == null || category == null) return NotFound();
        product.Categories.Add(category);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("GetCategoriesByProductId/{id}")]
    public async Task<IActionResult> GetCategoriesByProductId(long id)
    {
        var result = await _context.Products.FindAsync(id);
        if (result == null) return NotFound();
        var categories = _context.Categories.Where(category => category.Products.Any(product => product.Id == id));
        if (categories == null) return Ok(new List<Category>());
        return Ok(categories);
    }
}
