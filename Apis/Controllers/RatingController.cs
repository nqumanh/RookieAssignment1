using Apis.Data;
using Apis.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedViewModels;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class RatingController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly BookStoreContext _context;
    public RatingController(BookStoreContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<IEnumerable<RatingDTO>>> GetAll(int id)
    {
        var ratings = await _context.Ratings!
                         .Include(x => x.Product)
                         .Include(x => x.User)
                         .Where(x => x.Product.Id == id)
                         .ToListAsync();
        return _mapper.Map<List<RatingDTO>>(ratings);
    }
}