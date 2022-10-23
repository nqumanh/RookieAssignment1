using Apis.Data;
using Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly BookStoreContext _context;
    public UserController(BookStoreContext context)
    {
        _context = context;
    }

    // [HttpPost("Register")]
    // public async Task<ActionResult> Register()
    // {
    //     if (_context.Categories == null)
    //     {
    //         return NotFound();
    //     }

    //     var categories = await _context.Categories.ToListAsync();

    //     return Ok(categories);
    // }
}
