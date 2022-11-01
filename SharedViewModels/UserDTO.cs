using System.ComponentModel.DataAnnotations;

namespace SharedViewModels;

public class UserDTO
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string UserName { get; set; } = null!;
}