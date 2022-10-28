using Apis.Data;
using Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using SharedViewModels;

namespace Apis.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly BookStoreContext _context;
    private readonly IConfiguration _configuration;
    public UserController(BookStoreContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpGet("[action]")]

    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        return await _context.Users!
                        .Where(x => x.Role == UserRole.Customer)
                        .ToListAsync();
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<RegisterFormDTO>> Register(RegisterFormDTO userDTO)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        var user = new User
        {
            Role = UserRole.Customer,
            Name = userDTO.Name,
            Username = userDTO.Username,
            Password = HashPassword(userDTO.Password, salt),
            PasswordSalt = salt,
            Email = userDTO.Email,
            Address = userDTO.Address
        };

        _context.Users!.Add(user);
        await _context.SaveChangesAsync();

        return Ok(userDTO);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginFormDTO userDTO)
    {
        var user = await _context.Users!.Where(x => x.Username == userDTO.Username).FirstOrDefaultAsync();
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        if (!VerifyPasswordHash(userDTO.Password, user.Password, user.PasswordSalt))
        {
            return BadRequest("Wrong password");
        }

        var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString()),
            };

        var token = CreateToken(authClaims);

        return Ok(new
        {
            Id = user.Id,
            Name = user.Name,
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        });
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

    private string HashPassword(string password, byte[] salt)
    {
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return hash;
    }

    private bool VerifyPasswordHash(string requestPassword, string hashedPassword, byte[] salt)
    {
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: requestPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hash == hashedPassword;
    }
}
