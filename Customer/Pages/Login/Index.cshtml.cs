using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CustomerSite.Helper;
using Newtonsoft.Json;
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

        var definition = new
        {
            Name = "",
            Id = "",
            AccessToken = "",
            Expiration = new DateTime()
        };
        if ((int)response.StatusCode != 200)
        {
            return Page();
        }
        var info = JsonConvert.DeserializeAnonymousType(result, definition);

        CookieOptions options = new CookieOptions();
        options.Expires = DateTime.Now.AddDays(1);

        if (info!.AccessToken == null )
        {
            return Page();
        }

        if ((int)response.StatusCode == 200)
        {
            Response.Cookies.Append("access_token", info.AccessToken, options);
            Response.Cookies.Append("name", info.Name, options);
            Response.Cookies.Append("Id", info.Id, options);
            return RedirectToPage("../Index");
        }
        return Page();
    }
}

