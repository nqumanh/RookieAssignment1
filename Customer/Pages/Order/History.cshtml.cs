using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using CustomerSite.Helper;
using CustomerSite.Pages.Models;
using System.Text;
using SharedViewModels;

namespace CustomerSite.Pages;

public class OrderHistoryModel : PageModel
{
    private APIHelper _api = new APIHelper();

    private readonly ILogger<OrderHistoryModel> _logger;

    public OrderHistoryModel(ILogger<OrderHistoryModel> logger)
    {
        _logger = logger;
    }
    [BindProperty]
    public List<OrderDTO> OrderHistory { get; set; } = null!;
    public async Task<IActionResult> OnGetAsync()
    {
        var UserId = HttpContext!.Request.Cookies["Id"]!;
        HttpClient client = _api.initial();
        var response = await client.GetAsync($"User/OrderHistory/{UserId}");
        var result = response.Content.ReadAsStringAsync().Result;
        OrderHistory = JsonConvert.DeserializeObject<List<OrderDTO>>(result) ?? new List<OrderDTO>();

        return Page();
    }
}

