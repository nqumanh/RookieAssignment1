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
        var definition = new { Name = "", AccessToken = "" };
        var obj = JsonConvert.DeserializeAnonymousType(result, definition);

        if ((int)response.StatusCode != 200)
            return Page();

        HttpContext.Session.SetString("jwt", obj.AccessToken);
        HttpContext.Session.SetString("name", obj.Name);

        return RedirectToPage("../Index");
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (string.IsNullOrEmpty(HttpContext!.Session.GetString("jwt")))
        {
            return Page();
        }
        return RedirectToPage("../Index");
    }
}

