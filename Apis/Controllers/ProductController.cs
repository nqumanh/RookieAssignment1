using Apis.Data;
using Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedViewModels;

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
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
    {
        return await _context.Products!.Include(x => x.Category)
            .Select(x => ProductDTO(x))
            .ToListAsync();
    }

    [HttpGet("GetProductById/{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductById(int id)
    {
        var product = await _context.Products!.Include(x => x.Category).FirstOrDefaultAsync(product => product.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return ProductDTO(product);
    }

    [HttpPost("AddProduct")]
    public async Task<ActionResult<Product>> CreateTodoItem(Product product)
    {
        _context.Products!.Add(product);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private static ProductDTO ProductDTO(Product product) =>
        new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Image = product.Image,
            Author = product.Author,
            Price = product.Price,
            Quantity = product.Quantity,
            CreatedDate = product.CreatedDate,
            UpdatedDate = product.UpdatedDate,
            Category = product.Category!.Name
        };
}
