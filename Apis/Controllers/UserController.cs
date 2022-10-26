using Apis.Data;
using Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using SharedViewModels;

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

    // [HttpPost("GetUser/{id}")]
    // public async Task<ActionResult<RegisterFormDTO>> GetUser(int id)
    // {
    //     var user = await _context.Users!.FindAsync(id);

    //     if (user == null)
    //     {
    //         return NotFound();
    //     }

    //     return RegisterFormDTO(user);
    // }

    [HttpPost("[action]")]
    public async Task<ActionResult<RegisterFormDTO>> CreateUser(RegisterFormDTO userDTO)
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
    public async Task<ActionResult<LoginFormDTO>> Login(LoginFormDTO userDTO)
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

        string token = "token ne";
        // string token = CreateToken(user);
        return Ok(token);
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

    // private static RegisterFormDTO RegisterFormDTO(User user) =>
    //     new RegisterFormDTO
    //     {
    //         Name = user.Name,
    //         Username = user.Username,
    //         Password = user.Password,
    //         Email = user.Email,
    //         Address = user.Address
    //     };
}
