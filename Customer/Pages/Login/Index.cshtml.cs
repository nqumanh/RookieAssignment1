using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CustomerSite.Helper;
using SharedViewModels;

namespace CustomerSite.Pages;

public class LoginModel : PageModel
{
    private APIHelper _api = new APIHelper();
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(ILogger<LoginModel> logger)
    {
        _logger = logger;
    }

    [BindProperty]
    public LoginFormDTO? LoginForm { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        HttpClient client = _api.initial();
        var response = await client.PostAsJsonAsync("User/Login", LoginForm);
        var result = response.Content.ReadAsStringAsync().Result;

        CookieOptions options = new CookieOptions();
        options.Expires = DateTime.Now.AddDays(1);

        if ((int)response.StatusCode == 200)
        {
            Response.Cookies.Append("access_token", result, options);
            return RedirectToPage("../Index");
        }
        return Page();
    }
}

