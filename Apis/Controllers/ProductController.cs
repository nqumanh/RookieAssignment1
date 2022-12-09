using Apis.Interface;
using Apis.Models;
using Apis.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using SharedViewModels;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<PagedResponseModel<ProductDTO>>> GetProducts([FromQuery] ProductParameters productParameters)
    {
        var products = await _productService.GetProductsAsync(productParameters);
        return Ok(products);
    }
}

// [HttpGet("[action]")]
// public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
// {
//     return await _context.Products!
//                     .Include(x => x.Category)
//                     .Include(x => x.Ratings)
//                     .Select(x => ProductDTO(x))
//                     .ToListAsync();
// }

// [HttpPost("[action]")]
// public async Task<ActionResult<ProductDTO>> Create(ProductDTO productDTO)
// {
//     DateTime time = DateTime.Now;

//     var product = new Product
//     {
//         Name = productDTO.Name,
//         Description = productDTO.Description,
//         Image = productDTO.Image,
//         Author = productDTO.Author,
//         Price = productDTO.Price,
//         Quantity = productDTO.Quantity,
//         CreatedDate = time,
//         UpdatedDate = time
//     };
//     _context.Products!.Add(product);
//     await _context.SaveChangesAsync();

//     return CreatedAtAction(
//             nameof(Read),
//             new { id = product.Id },
//             ProductDTO(product));
// }

// [HttpGet("[action]/{id}")]
// public async Task<ActionResult<ProductDTO>> Read(int id)
// {
//     var product = await _context.Products!
//                             .Include(x => x.Category)
//                             .Include(x => x.Ratings)
//                             .FirstOrDefaultAsync(product => product.Id == id);

//     if (product == null)
//     {
//         return NotFound();
//     }

//     return ProductDTO(product);
// }

// [HttpPut("[action]/{id}")]
// public async Task<ActionResult<ProductDTO>> Update(int id, ProductDTO productDTO)
// {
//     if (id != productDTO.Id)
//     {
//         return BadRequest();
//     }

//     var product = await _context.Products!.FindAsync(id);
//     if (product == null)
//     {
//         return NotFound();
//     }
//     var category = (productDTO.CategoryId != null) ? await _context.Categories!.FindAsync(Int32.Parse(productDTO.CategoryId)) : null;

//     product.Name = productDTO.Name;
//     product.Description = productDTO.Description;
//     product.Image = productDTO.Image;
//     product.Author = productDTO.Author;
//     product.Price = productDTO.Price;
//     product.Quantity = productDTO.Quantity;
//     product.UpdatedDate = DateTime.Now;
//     product.Category = category;

//     try
//     {
//         await _context.SaveChangesAsync();
//     }
//     catch (DbUpdateConcurrencyException) when (!ProductExists(id))
//     {
//         return NotFound();
//     }

//     return CreatedAtAction(
//             nameof(Read),
//             new { id = product.Id },
//             ProductDTO(product));
// }

// [HttpDelete("[action]/{id}")]
// public async Task<IActionResult> Delete(int id)
// {
//     var product = await _context.Products!.FindAsync(id);

//     if (product == null)
//     {
//         return NotFound();
//     }

//     _context.Products.Remove(product);
//     await _context.SaveChangesAsync();

//     return NoContent();
// }

// private bool ProductExists(int id)
// {
//     return _context.Products!.Any(e => e.Id == id);
// }

// private static RatingDTO RatingDTO(Rating rating) =>
//     new RatingDTO
//     {
//         Star = rating.Star,
//         Comment = rating.Comment,
//         Reviewer = rating.User.Name,
//         UpdatedDate = rating.UpdatedDate
//     };

// private static ProductDTO ProductDTO(Product product)
// {
//     var numberOfRating = product.Ratings.Count;
//     var avgRating = numberOfRating > 0 ? Convert.ToDecimal(product.Ratings.Aggregate(0, (sum, rating) => sum + rating.Star)) / numberOfRating : 0;
//     var categoryId = (product.Category == null) ? null : product.Category.Id.ToString();
//     var categoryName = (product.Category == null) ? null : product.Category.Name;
//     return new ProductDTO
//     {
//         Id = product.Id,
//         Name = product.Name,
//         Description = product.Description,
//         Image = product.Image,
//         Author = product.Author,
//         AverageRating = avgRating,
//         Price = product.Price,
//         Quantity = product.Quantity,
//         CategoryId = categoryId,
//         CategoryName = categoryName,
//         CreatedDate = product.CreatedDate,
//         UpdatedDate = product.UpdatedDate
//     };
