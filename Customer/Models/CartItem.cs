namespace CustomerSite.Pages.Models;

public class CartItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
}