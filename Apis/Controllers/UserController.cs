using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Apis.Data;
using Apis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SharedViewModels;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly BookStoreContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    public UserController(BookStoreContext context, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register(RegisterFormDTO input)
    {
        if (ModelState.IsValid)
        {
            var user = new User { UserName = input.UserName, Email = input.Email, Name = input.Name, PhoneNumber = input.PhoneNumber, Address = input.Address };
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "customer");
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginFormDTO input)
    {
        var result = await _signInManager.PasswordSignInAsync(input.Username,
                           input.Password, isPersistent: false, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(input.Username);
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.StreetAddress, user.Address),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = CreateToken(authClaims);

            return Ok(new
            {
                Id = user.Id,
                Name = user.Name,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }
        else
        {
            return BadRequest("Login failed!");
        }
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<ActionResult<RatingDTO>> WriteReview(ReviewFormDTO reviewForm)
    {
        if (reviewForm.Star < 1 || reviewForm.Star > 5)
            return BadRequest("Click a star to rate");

        var product = await _context.Products!.FindAsync(reviewForm.ProductId);

        if (product == null)
        {
            return BadRequest("Product does not exist.");
        }

        var claimsPrincipal = HttpContext.User;
        var user = _userManager.Users.FirstOrDefault(x => x.Id == reviewForm.UserId);

        if (user == null)
            return BadRequest("Require Login");

        DateTime time = DateTime.Now;
        var rating = new Rating
        {
            Star = reviewForm.Star,
            Comment = reviewForm.Comment,
            CreatedDate = time,
            UpdatedDate = time,
            Product = product,
            User = user
        };

        _context.Ratings!.Add(rating);
        await _context.SaveChangesAsync();

        return Ok(RatingDTO(rating));
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<ActionResult<OrderDTO>> Order(OrderDTO orderDTO)
    {
        var claimsPrincipal = HttpContext.User;
        var user = _userManager.Users.FirstOrDefault(x => x.Id == orderDTO.UserId);

        if (user == null)
            return BadRequest("Require Login");

        var cart = new List<OrderLine>();
        foreach (var item in orderDTO.Cart)
        {
            var product = await _context.Products!.FindAsync(item.ProductId);
            if (product == null) return BadRequest("Product not found");
            var orderLine = new OrderLine
            {
                Quantity = item.Quantity,
                Product = product,
            };
            cart.Add(orderLine);
        }

        DateTime time = DateTime.Now;
        var order = new Order
        {
            User = user,
            Address = orderDTO.Address,
            OrderLines = cart,
            Time = time
        };

        _context.Orders!.Add(order);
        await _context.SaveChangesAsync();

        return Ok(OrderDTO(order));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    private static RatingDTO RatingDTO(Rating rating) =>
        new RatingDTO
        {
            Star = rating.Star,
            Comment = rating.Comment,
            Reviewer = rating.User.Name,
            UpdatedDate = rating.UpdatedDate
        };

    private static OrderDTO OrderDTO(Order order) =>
        new OrderDTO
        {
            UserId = order.User.Id,
            Address = order.Address,
            Cart = order.OrderLines!.Select(x => CartItemDTO(x)).ToList()
        };

    private static CartItemDTO CartItemDTO(OrderLine orderLine) =>
        new CartItemDTO
        {
            ProductId = orderLine.Product.Id,
            Quantity = orderLine.Quantity
        };

    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SymmetricKey"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddMinutes(30),
            claims: authClaims,
            signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256));

        return token;
    }
}
