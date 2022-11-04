using System.ComponentModel.DataAnnotations;

namespace Apis.Models;

public class Rating
{
    public int Id { get; set; }
    [Required]
    public int Star { get; set; }
    public string? Comment { get; set; }
    [DataType(DataType.Date)]
    public DateTime CreatedDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime UpdatedDate { get; set; }
    public Product Product { get; set; } = new Product();
    public User User { get; set; } = null!;
}