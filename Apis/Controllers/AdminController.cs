using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Apis.Data;
using Apis.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SharedViewModels;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly BookStoreContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(
        IConfiguration configuration,
        BookStoreContext context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginFormDTO input)
    {
        var result = await _signInManager.PasswordSignInAsync(input.Username,
                           input.Password, isPersistent: false, lockoutOnFailure: true);

        if (!result.Succeeded)
            return BadRequest("Invalid Account!");

        var user = await _userManager.FindByNameAsync(input.Username);
        var userRoles = await _userManager.GetRolesAsync(user);

        if (!userRoles.Any(x => x == "admin"))
        {
            return BadRequest("You're not authorized!");
        }

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
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddRole(string name)
    {
        IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
        if (result.Succeeded)
            return Ok();
        else
            return BadRequest(result);
    }


    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
    {
        var users = await _userManager.GetUsersInRoleAsync("customer");
        return _mapper.Map<List<UserDTO>>(users);
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
            expires: DateTime.Now.AddMinutes(30),
            claims: authClaims,
            signingCredentials: new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256));

        return token;
    }
}
