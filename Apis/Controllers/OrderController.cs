using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Apis.Data;
using Apis.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedViewModels;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly BookStoreContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public OrderController(BookStoreContext context, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<OrderDTO>> Get(int id)
    {
        var order = await _context.Orders!
            .Include(x => x.OrderLines)
            .FirstOrDefaultAsync(order => order.Id == id);
        return _mapper.Map<OrderDTO>(order);
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<List<OrderLineDTO>>> GetCart(int id)
    {
        return await _context.OrderLines!
            .Include(x => x.Product)
            .Include(x => x.Order)
            .Where(x => x.Order.Id == id)
            .Select(x => OrderLineDTO(x))
            .ToListAsync();
    }

    private static OrderLineDTO OrderLineDTO(OrderLine orderLine) =>
        new OrderLineDTO
        {
            ProductName = orderLine.Product.Name,
            Image = orderLine.Product.Image!,
            Quantity = orderLine.Quantity,
            Price = orderLine.Price
        };
}
