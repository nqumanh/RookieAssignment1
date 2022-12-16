namespace SharedViewModels;

public class PagedResponseModel<TModel>
{
    public int TotalItems { get; set; }

    public IEnumerable<TModel> Items { get; set; } = null!;
}