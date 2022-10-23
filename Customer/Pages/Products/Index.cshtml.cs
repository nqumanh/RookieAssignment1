using CustomerSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerSite.Pages;
public class ProductModel : PageModel
{
    private readonly ILogger<ProductModel> _logger;

    public ProductModel(ILogger<ProductModel> logger)
    {
        _logger = logger;
    }

    public List<Product>? Products = new List<Product>();
    public List<Category>? Categories = new List<Category>();
    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }
    public SelectList? OptionCategories { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? SelectedCategory { get; set; }

    public async Task OnGetAsync()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7133/");
        var response = await client.GetAsync("Product/GetAllProducts");
        var result = response.Content.ReadAsStringAsync().Result;
        Products = JsonConvert.DeserializeObject<List<Product>>(result);

        response = await client.GetAsync("Category/GetAllCategories");
        result = response.Content.ReadAsStringAsync().Result;
        Categories = JsonConvert.DeserializeObject<List<Category>>(result);

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
