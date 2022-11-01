namespace Apis.Models;

public class Order
{
    public int Id { get; set; }
    public ICollection<OrderLine>? OrderLines { get; set; }
    public User User { get; set; } = null!;
}