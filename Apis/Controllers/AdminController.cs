using Apis.Data;
using Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SharedViewModels;
using AutoMapper;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly BookStoreContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(
        BookStoreContext context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginFormDTO input)
    {
        var result = await _signInManager.PasswordSignInAsync(input.Username,
                           input.Password, isPersistent: false, lockoutOnFailure: true);

        if (!result.Succeeded)
            return BadRequest("Invalid Account!");

        // var user = _userManager.FindByNameAsync(input.Username);
        // if (user == null)
        // {
        //     return BadRequest("Invalid Account!");
        // }
        // if (!User.IsInRole("admin"))
        // {
        //     return BadRequest("You're not an admin");
        // }

        return Ok("Login Successfully!");
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
}
