using System.ComponentModel.DataAnnotations;

namespace SharedViewModels;

public class LoginFormDTO
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}