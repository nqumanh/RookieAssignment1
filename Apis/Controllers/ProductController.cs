using Apis.Data;
using Apis.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedViewModels;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly BookStoreContext _context;
    public ProductController(BookStoreContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
    {
        var product = await _context.Products!
                        .Include(x => x.Category)
                        .Include(x => x.Ratings)
                        .ToListAsync();
        return _mapper.Map<List<ProductDTO>>(product);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<ProductDTO>> Create(ProductDTO productDTO)
    {
        DateTime time = DateTime.Now;

        var product = _mapper.Map<Product>(productDTO);
        product.CreatedDate = time;
        product.UpdatedDate = time;
        _context.Products!.Add(product);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
                nameof(Read),
                new { id = product.Id },
                ProductDTO(product));
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<ProductDTO>> Read(int id)
    {
        var product = await _context.Products!
                                .Include(x => x.Category)
                                .Include(x => x.Ratings)
                                .FirstOrDefaultAsync(product => product.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return ProductDTO(product);
    }

    [HttpPut("[action]/{id}")]
    public async Task<ActionResult<ProductDTO>> Update(int id, ProductDTO productDTO)
    {
        if (id != productDTO.Id)
        {
            return BadRequest();
        }

        var product = await _context.Products!.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        var category = (productDTO.CategoryId != null) ? await _context.Categories!.FindAsync(Int32.Parse(productDTO.CategoryId)) : null;

        DateTime time = DateTime.Now;

        product.Name = productDTO.Name;
        product.Description = productDTO.Description;
        product.Image = productDTO.Image;
        product.Author = productDTO.Author;
        product.Price = productDTO.Price;
        product.Quantity = productDTO.Quantity;
        product.UpdatedDate = time;
        product.Category = category;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ProductExists(id))
        {
            return NotFound();
        }

        return CreatedAtAction(
                nameof(Read),
                new { id = product.Id },
                ProductDTO(product));
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products!.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return _context.Products!.Any(e => e.Id == id);
    }

    private ProductDTO ProductDTO(Product product)
    {
        var productDTO = _mapper.Map<ProductDTO>(product);
        var avgRating = product.Ratings.Count > 0 ?
            Convert.ToDecimal(product.Ratings.Aggregate(0, (sum, rating) => sum + rating.Star)) / product.Ratings.Count
            : 0;
        if (product.Category != null)
        {
            productDTO.CategoryId = product.Category.Id.ToString();
            productDTO.CategoryName = product.Category.Name;
        }
        return productDTO;
    }
}
