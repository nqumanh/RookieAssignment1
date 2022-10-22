using Apis.Data;
using Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apis.Controllers;

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
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        if (_context.Products == null)
        {
            return NotFound();
        }

        var products = await (from p in _context.Products
                              join c in _context.Categories
                              on p.Category.Id equals c.Id
                              select new
                              {
                                  Name = p.Name,
                                  Description = p.Description,
                                  Image = p.Image,
                                  Author = p.Author,
                                  Price = p.Price,
                                  Quantity = p.Quantity,
                                  CreatedDate = p.CreatedDate,
                                  UpdatedDate = p.UpdatedDate,
                                  Category = c.Name
                              }).ToListAsync();
        return Ok(products);
    }

    [HttpPost("AddProduct")]
    public async Task<ActionResult<Product>> CreateTodoItem(Product product)
    {
        _context.Products!.Add(product);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
