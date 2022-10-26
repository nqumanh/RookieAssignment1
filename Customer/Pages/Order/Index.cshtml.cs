using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerSite.Pages;

public class OrderModel : PageModel
{
    private readonly ILogger<OrderModel> _logger;

    public OrderModel(ILogger<OrderModel> logger)
    {
        _logger = logger;
    }
}

