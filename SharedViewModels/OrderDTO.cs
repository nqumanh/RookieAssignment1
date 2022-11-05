using System.ComponentModel.DataAnnotations;

namespace SharedViewModels;

public class OrderDTO
{
    [Required]
    public string UserId { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Message { get; set; } = string.Empty;
    public List<CartItemDTO> Cart { get; set; } = new List<CartItemDTO>();
}