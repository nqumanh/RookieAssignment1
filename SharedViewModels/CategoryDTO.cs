using System.ComponentModel.DataAnnotations;

namespace SharedViewModels;

public class CategoryDTO
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}