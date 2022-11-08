using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using CustomerSite.Helper;
using CustomerSite.Pages.Models;
using System.Text;
using SharedViewModels;

namespace CustomerSite.Pages;

public class OrderDetailModel : PageModel
{
    private APIHelper _api = new APIHelper();

    private readonly ILogger<OrderDetailModel> _logger;

    public OrderDetailModel(ILogger<OrderDetailModel> logger)
    {
        _logger = logger;
    }
    public decimal TotalPrice = 0;
    public List<OrderLineDTO> Cart = new List<OrderLineDTO>();
    public OrderDTO Order { get; set; } = default!;

    public async Task OnGetAsync(int? id)
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync($"Order/Get/{id}");
        var result = response.Content.ReadAsStringAsync().Result;
        Order = JsonConvert.DeserializeObject<OrderDTO>(result) ?? new OrderDTO();

        response = await client.GetAsync($"Order/GetCart/{id}");
        result = response.Content.ReadAsStringAsync().Result;
        Cart = JsonConvert.DeserializeObject<List<OrderLineDTO>>(result) ?? new List<OrderLineDTO>();
        foreach (var item in Cart)
        {
            TotalPrice += item.Quantity * item.Price;
        }
    }
}

