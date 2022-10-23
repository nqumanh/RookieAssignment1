namespace Apis.Models;

public class Order
{
    public int Id { get; set; }
    public User User { get; set; } = new User();
    public ICollection<OrderLine>? OrderLines { get; set; }
}