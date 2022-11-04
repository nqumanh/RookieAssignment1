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

        if ((int)response.StatusCode != 200)
        {
            TempData["error"] = "Login failed!";
            return Page();
        }

        var definition = new { Id = "", Name = "", AccessToken = "" };
        var obj = JsonConvert.DeserializeAnonymousType(result, definition);

        if (obj != null)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Append("Id", obj.Id, options);
            Response.Cookies.Append("Name", obj.Name, options);
            Response.Cookies.Append("AccessToken", obj.AccessToken, options);
        }

        TempData["success"] = "Login Successfully";
        return RedirectToPage("../Index");
    }

    public IActionResult OnGetAsync()
    {
        if (!string.IsNullOrEmpty(HttpContext.Request!.Cookies["AccessToken"]))
            return RedirectToPage("../Index");
        return Page();
    }
}

