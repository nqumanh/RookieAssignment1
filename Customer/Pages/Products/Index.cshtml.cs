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
    private readonly IConfiguration Configuration;

    public ProductModel(ILogger<ProductModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        Configuration = configuration;
    }

    public List<ProductDTO>? Products = new List<ProductDTO>();
    public List<CategoryDTO>? Categories = new List<CategoryDTO>();
    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }
    public SelectList? OptionCategories { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? SelectedCategory { get; set; }

    public async Task OnGetAsync()
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync("Product/GetAllProducts");
        var result = response.Content.ReadAsStringAsync().Result;
        Products = JsonConvert.DeserializeObject<List<ProductDTO>>(result);

        response = await client.GetAsync("Category/GetAllCategories");
        result = response.Content.ReadAsStringAsync().Result;
        Categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(result);

        if (!string.IsNullOrEmpty(SearchString))
        {
            Products = Products!.Where(s => s.Name.ToLower().Contains(SearchString.ToLower())).ToList();
        }

        if (!string.IsNullOrEmpty(SelectedCategory))
        {
            Products = Products!.Where(x =>
            {
                return (x.Category == SelectedCategory);
            }).ToList();
        }

        OptionCategories = new SelectList(Categories?.Select(x => x.Name));
    }
}
