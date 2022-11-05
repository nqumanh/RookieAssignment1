using System.ComponentModel.DataAnnotations;

namespace SharedViewModels;

public class OrderFormDTO
{
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    public string Message { get; set; } = string.Empty;
}