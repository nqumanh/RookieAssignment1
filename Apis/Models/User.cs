namespace Apis.Models;

public enum UserRole
{
    Customer = 1,
    Admin = 2
}
public class User
{
    public int Id { get; set; }
    public UserRole Role { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

}