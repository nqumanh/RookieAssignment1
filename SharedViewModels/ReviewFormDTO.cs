using System.ComponentModel.DataAnnotations;

namespace SharedViewModels;

public class ReviewFormDTO
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Star { get; set; }
    public string? Title { get; set; }
    public string? Comment { get; set; }
}