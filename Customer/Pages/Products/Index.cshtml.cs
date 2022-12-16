using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using CustomerSite.Helper;
using SharedViewModels;

namespace CustomerSite.Pages;
public class ProductModel : PageModel
{
    private APIHelper _api = new APIHelper();
    private readonly ILogger<ProductModel> _logger;
    private readonly IConfiguration _configuration;

    public ProductModel(ILogger<ProductModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IEnumerable<ProductDTO>? Products = new List<ProductDTO>();
    public List<CategoryDTO>? Categories = new List<CategoryDTO>();
    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }
    public SelectList? OptionCategories { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? SelectedCategory { get; set; }
    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;
    [BindProperty(SupportsGet = true)]
    public int TotalPage { get; set; } = 0;
    // public PaginatedList<ProductDTO> paginatedProducts { get; set; } = null!;
    public async Task OnGetAsync(int pageIndex = 1)
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync($"Product/GetProducts?PageSize=4&PageNumber={pageIndex}");
        CurrentPage = pageIndex;
        Console.WriteLine(1111111111111111111);
        Console.WriteLine(pageIndex);
        var result = response.Content.ReadAsStringAsync().Result;

        Console.WriteLine(result);
        var pagingProduct = JsonConvert.DeserializeObject<PagedResponseModel<ProductDTO>>(result);

        TotalPage = pagingProduct == null ? 0 : Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(pagingProduct.TotalItems) / 4));
        Products = pagingProduct?.Items;

        response = await client.GetAsync("Category/GetAll");
        result = response.Content.ReadAsStringAsync().Result;
        Categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(result);

        if (!string.IsNullOrEmpty(SearchString))
        {
            Products = Products!.Where(s => s.Name.ToLower().Contains(SearchString.ToLower())).ToList();
        }

        if (!string.IsNullOrEmpty(SelectedCategory))
        {
            Products = Products!.Where(x => x.CategoryName == SelectedCategory).ToList();
        }

        OptionCategories = new SelectList(Categories?.Select(x => x.Name));
    }
}
