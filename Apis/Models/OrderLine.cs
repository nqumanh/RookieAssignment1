namespace Apis.Models;

public class OrderLine
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public Order Order { get; set; } = new Order();
    public Product Product { get; set; } = new Product();
}