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
}
