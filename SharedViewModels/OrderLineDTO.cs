namespace SharedViewModels;

public class OrderLineDTO
{
    public string ProductName { get; set; } = null!;
    public string? Image { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}