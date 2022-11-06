using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using CustomerSite.Helper;
using SharedViewModels;

namespace CustomerSite.Pages;
public class IndexModel : PageModel
{
    private APIHelper _api = new APIHelper();
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }
    public List<ProductDTO>? Products = new List<ProductDTO>();
    public List<CategoryDTO>? Categories = new List<CategoryDTO>();

    public async Task<IActionResult> OnGetAsync()
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync("Product/GetAll");
        var result = response.Content.ReadAsStringAsync().Result;
        Console.WriteLine(result);
        Products = JsonConvert.DeserializeObject<List<ProductDTO>>(result);

        response = await client.GetAsync("Category/GetAll");
        result = response.Content.ReadAsStringAsync().Result;
        Categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(result);

        return Page();
    }

    public async Task<IActionResult> OnGetLogoutAsync()
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync("User/Logout");

        HttpContext.Response.Cookies.Delete("AccessToken");
        HttpContext.Response.Cookies.Delete("Name");
        HttpContext.Response.Cookies.Delete("Id");

        TempData["success"] = "Logout!";
        return RedirectToPage("/Index");
    }

}