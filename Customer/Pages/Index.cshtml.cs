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
    public IEnumerable<ProductDTO>? Products = new List<ProductDTO>();
    public List<CategoryDTO>? Categories = new List<CategoryDTO>();

    public async Task<IActionResult> OnGetAsync()
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync("Product/GetProducts?PageSize=6&PageNumber=1");
        var result = response.Content.ReadAsStringAsync().Result;
        var pagingProduct = JsonConvert.DeserializeObject<PagedResponseModel<ProductDTO>>(result);

        Products = pagingProduct?.Items;

        Products = Products!.OrderByDescending(x => x.AverageRating).ToList().GetRange(0, 6);

        response = await client.GetAsync("Category/GetAll");
        result = response.Content.ReadAsStringAsync().Result;
        Categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(result);

        return Page();
    }

    public async Task<IActionResult> OnGetLogoutAsync()
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync("User/Logout");

        HttpContext.Session.Remove("AccessToken");
        HttpContext.Session.Remove("Name");
        HttpContext.Session.Remove("Id");

        TempData["success"] = "Logout!";
        return RedirectToPage("/Index");
    }

}