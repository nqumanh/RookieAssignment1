using System.ComponentModel.DataAnnotations;

namespace SharedViewModels;

public class RatingDTO
{
    public string? Title { get; set; }
    public int Star { get; set; }
    public string? Comment { get; set; }
    public string Reviewer { get; set; } = string.Empty;
    [DataType(DataType.Date)]
    public DateTime UpdatedDate { get; set; }
}