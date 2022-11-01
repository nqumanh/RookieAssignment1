using Microsoft.AspNetCore.Identity;

namespace Apis.Models;
public class User : IdentityUser
{
    public UserRole Role { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public List<Rating>? Ratings { get; set; }
    public List<Order>? Orders { get; set; }
}