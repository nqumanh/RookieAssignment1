using System.ComponentModel.DataAnnotations;

namespace Apis.Models;

public class Order
{
    public int Id { get; set; }
    [Required]
    public User User { get; set; } = null!;
    [Required]
    public string Address { get; set; } = string.Empty;
    public ICollection<OrderLine>? OrderLines { get; set; }
}