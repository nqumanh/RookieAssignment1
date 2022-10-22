using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerSite.Models;

public class Product
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string Author { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime UpdatedDate { get; set; }
    public string? Category { get; set; }
}