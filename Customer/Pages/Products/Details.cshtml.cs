using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SharedViewModels;

namespace CustomerSite.Pages;

public class DetailsModel : PageModel
{
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ILogger<DetailsModel> logger)
    {
        _logger = logger;
    }

    public ProductDTO Product { get; set; } = default!;

    public async Task OnGetAsync(int? id)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7133/");
        var response = await client.GetAsync($"Product/GetProductById/{id}");
        var result = response.Content.ReadAsStringAsync().Result;
        Product = JsonConvert.DeserializeObject<ProductDTO>(result);
    }
}