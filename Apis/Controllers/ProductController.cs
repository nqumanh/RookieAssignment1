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

    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
    {
        return await _context.Products!
                        .Include(x => x.Category)
                        .Include(x => x.Ratings)
                        .Select(x => ProductDTO(x))
                        .ToListAsync();
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<ProductDTO>> GetProductById(int id)
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

    [HttpPost("[action]")]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Products!.Add(product);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<IEnumerable<RatingDTO>>> GetAllRatingsOfProduct(int id)
    {
        return await _context.Ratings!
                        .Include(x => x.Product)
                        .Include(x => x.User)
                        .Where(x => x.Product.Id == id)
                        .Select(x => RatingDTO(x))
                        .ToListAsync();
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<Rating>> WriteReview(ReviewFormDTO reviewForm)
    {
        var product = await _context.Products!.FindAsync(reviewForm.ProductId);
        var user = await _context.Users!.FindAsync(reviewForm.UserId);
        if (user == null || product == null)
            return BadRequest();
        var rating = new Rating
        {
            Star = reviewForm.Star,
            Title = reviewForm.Title,
            Comment = reviewForm.Comment,
            UpdatedDate = new DateTime(),
            Product = product,
            User = user
        };
        _context.Ratings!.Add(rating);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private static RatingDTO RatingDTO(Rating rating) =>
        new RatingDTO
        {
            Star = rating.Star,
            Title = rating.Title,
            Comment = rating.Comment,
            Reviewer = rating.User.Name,
            UpdatedDate = rating.UpdatedDate
        };

    private static ProductDTO ProductDTO(Product product)
    {
        var numberOfRating = product.Ratings.Count;
        var avgRating = numberOfRating > 0 ? Convert.ToDecimal(product.Ratings.Aggregate(0, (sum, rating) => sum + rating.Star)) / numberOfRating : 0;
        return new ProductDTO
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
            Category = product.Category!.Name,
            AverageRating = avgRating
        };
    }
}
