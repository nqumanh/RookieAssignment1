using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CustomerSite.Helper;
using SharedViewModels;

namespace CustomerSite.Pages;

public class RegisterModel : PageModel
{
    private APIHelper _api = new APIHelper();
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(ILogger<RegisterModel> logger)
    {
        _logger = logger;
    }

    // public string? ConfirmPassword { get; set; }
    [BindProperty]
    public RegisterFormDTO RegisterForm { get; set; } = new RegisterFormDTO();
    public async Task<IActionResult> OnPostAsync()
    {
        if (RegisterForm.UserName == null)
        {
            TempData["error"] = "UserName is required!";
            return Page();
        }
        if (RegisterForm.Password == null)
        {
            TempData["error"] = "Password is required!";
            return Page();
        }

        if (RegisterForm.Password != RegisterForm.ConfirmPassword)
        {
            TempData["error"] = "Confirm Password does not match!";
            return Page();
        }

        if (RegisterForm.Email == null)
        {
            TempData["error"] = "Email is required!";
            return Page();
        }

        if (RegisterForm.Name == null)
        {
            TempData["error"] = "Name is required!";
            return Page();
        }

        if (RegisterForm.PhoneNumber == null)
        {
            TempData["error"] = "PhoneNumber is required!";
            return Page();
        }

        if (RegisterForm.Address == null)
        {
            TempData["error"] = "Address is required!";
            return Page();
        }

        HttpClient client = _api.initial();
        var response = await client.PostAsJsonAsync("User/Register", RegisterForm);
        var result = response.Content.ReadAsStringAsync().Result;

        if ((int)response.StatusCode == 200)
        {
            TempData["success"] = "Register successfully";
            return RedirectToPage("../Login/Index");
        }
        else
        {
            TempData["error"] = result;
            return Page();
        }
    }

    public IActionResult OnGetAsync()
    {
        var session = HttpContext.Session;
        if (!string.IsNullOrEmpty(session.GetString("AccessToken")))
            return RedirectToPage("../Index");
        return Page();
    }
}

