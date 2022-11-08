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

    public List<ProductDTO>? Products = new List<ProductDTO>();
    public List<CategoryDTO>? Categories = new List<CategoryDTO>();
    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }
    public SelectList? OptionCategories { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? SelectedCategory { get; set; }
    public PaginatedList<ProductDTO> paginatedProducts { get; set; } = null!;
    public async Task OnGetAsync(int? pageIndex)
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync("Product/GetAll");
        var result = response.Content.ReadAsStringAsync().Result;
        Products = JsonConvert.DeserializeObject<List<ProductDTO>>(result);

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

        var pageSize = _configuration.GetValue("PageSize", 4);
        paginatedProducts = PaginatedList<ProductDTO>.Create(
            Products ?? new List<ProductDTO>(), pageIndex ?? 1, pageSize);
    }
}
