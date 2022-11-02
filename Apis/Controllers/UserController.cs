using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Apis.Data;
using Apis.Models;
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
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = CreateToken(authClaims);

            return Ok(new
            {
                Name = user.Name,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }
        else
        {
            return BadRequest("Login failed!");
        }
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SymmetricKey"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddMinutes(5),
            claims: authClaims,
            signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256));

        return token;
    }
}
