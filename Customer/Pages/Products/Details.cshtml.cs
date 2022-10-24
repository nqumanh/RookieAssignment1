using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SharedViewModels;
using CustomerSite.Helper;

namespace CustomerSite.Pages;
public class DetailsModel : PageModel
{
    private APIHelper _api = new APIHelper();
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ILogger<DetailsModel> logger)
    {
        _logger = logger;
    }

    public ProductDTO Product { get; set; } = default!;

    public async Task OnGetAsync(int? id)
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync($"Product/GetProductById/{id}");
        var result = response.Content.ReadAsStringAsync().Result;
        Product = JsonConvert.DeserializeObject<ProductDTO>(result) ?? new ProductDTO();
    }
}