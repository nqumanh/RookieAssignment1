namespace Apis.Models;
public class User
{
    public int Id { get; set; }
    public UserRole Role { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public byte[] PasswordSalt { get; set; } = new byte[]{};
    public string Address { get; set; } = string.Empty;

}