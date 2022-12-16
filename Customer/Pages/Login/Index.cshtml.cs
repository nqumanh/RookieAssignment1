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

        var session = HttpContext.Session;


        if (obj != null)
        {
            // CookieOptions options = new CookieOptions();
            // options.Expires = DateTime.Now.AddMinutes(30);
            session.SetString("Id", obj.Id);
            session.SetString("Name", obj.Name);
            session.SetString("AccessToken", obj.AccessToken);
            ViewData["Name"] = obj.Name;
        }

        TempData["success"] = "Login Successfully";
        return RedirectToPage("../Index");
    }

    public IActionResult OnGetAsync()
    {
        var session = HttpContext.Session;

        if (!string.IsNullOrEmpty(session.GetString("AccessToken")))
            return RedirectToPage("../Index");
        return Page();
    }
}

