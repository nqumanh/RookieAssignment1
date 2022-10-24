using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CustomerSite.Helper;
using SharedViewModels;

namespace CustomerSite.Pages;

public class LogoutModel : PageModel
{
    private APIHelper _api = new APIHelper();
    private readonly ILogger<LogoutModel> _logger;

    public LogoutModel(ILogger<LogoutModel> logger)
    {
        _logger = logger;
    }
    public async Task<IActionResult> OnPostAsync()
    {
        Response.Cookies.Delete("access_token");
        return Page();
    }
}

