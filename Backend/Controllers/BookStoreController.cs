using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Data;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class BookStoreController : ControllerBase
{
    private readonly BookStoreContext _context;

    public BookStoreController(BookStoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var result = await _context.Products!.ToListAsync();
        if (result == null) return BadRequest();
        return Ok(result);
    }
}
