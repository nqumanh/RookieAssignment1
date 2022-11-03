using System.ComponentModel.DataAnnotations;

namespace SharedViewModels;

public class ReviewFormDTO
{
    [Required]
    public string UserId { get; set; } = null!;
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Star { get; set; }
    public string? Comment { get; set; }
}