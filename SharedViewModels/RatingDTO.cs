using System.ComponentModel.DataAnnotations;

namespace SharedViewModels;

public class RatingDTO
{
    [Required]
    [Range(1,5)]
    public int Star { get; set; }
    [Required]
    public string? Comment { get; set; }
    public string Reviewer { get; set; } = string.Empty;
    [DataType(DataType.Date)]
    public DateTime UpdatedDate { get; set; }
}