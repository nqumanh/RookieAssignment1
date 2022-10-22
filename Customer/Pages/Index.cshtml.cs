using CustomerSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
namespace CustomerSite.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public List<Product>? Products = new List<Product>();
    public List<Category>? Categories = new List<Category>();

    public async Task<IActionResult> OnGetAsync()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7133/");
        var response = await client.GetAsync("Product/GetAllProducts");
        var result = response.Content.ReadAsStringAsync().Result;
        Products = JsonConvert.DeserializeObject<List<Product>>(result);

        response = await client.GetAsync("Category/GetAllCategories");
        result = response.Content.ReadAsStringAsync().Result;
        Categories = JsonConvert.DeserializeObject<List<Category>>(result);

        return Page();
    }
}
