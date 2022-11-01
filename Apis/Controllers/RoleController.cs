using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
        if (result.Succeeded)
            return Ok();
        else
            return BadRequest(result);
    }
}