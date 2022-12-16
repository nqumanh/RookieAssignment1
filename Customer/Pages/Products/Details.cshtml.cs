using Newtonsoft.Json;
using CustomerSite.Helper;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedViewModels;

namespace CustomerSite.Pages;
public class DetailsModel : PageModel
{
    private APIHelper _api = new APIHelper();
    private readonly ILogger<DetailsModel> _logger;

    public DetailsModel(ILogger<DetailsModel> logger)
    {
        _logger = logger;
    }

    public ProductDTO Product { get; set; } = default!;

    [BindProperty]
    public string Stars { get; set; } = "0";
    [BindProperty]
    public List<string> TitleList { get; set; } = new List<string>() { "Very Bad", "Poor", "OK", "Good", "Excellent" };
    [BindProperty]
    public ReviewFormDTO ReviewForm { get; set; } = default!;
    public List<RatingDTO> RatingList { get; set; } = new List<RatingDTO>();

    public async Task OnGetAsync(int? id)
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync($"Product/Read/{id}");
        var result = response.Content.ReadAsStringAsync().Result;
        Product = JsonConvert.DeserializeObject<ProductDTO>(result) ?? new ProductDTO();

        response = await client.GetAsync($"Rating/GetAll/{id}");
        result = response.Content.ReadAsStringAsync().Result;
        RatingList = JsonConvert.DeserializeObject<List<RatingDTO>>(result) ?? new List<RatingDTO>();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        ReviewForm.Star = Int32.Parse(Stars);
        if (ReviewForm.Star < 1 || ReviewForm.Star > 5)
        {
            TempData["error"] = "Click a star to rate!";
            return RedirectToPage();
        }

        ReviewForm.ProductId = id;

        var session = HttpContext.Session;
        ReviewForm.UserId = session.GetString("Id");

        var token = session.GetString("AccessToken");
        if (token == null)
        {
            TempData["error"] = "You have to login first!";
            return RedirectToPage();
        }

        HttpClient client = _api.initial();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("User/WriteReview", ReviewForm);
        var result = response.Content.ReadAsStringAsync().Result;

        if ((int)response.StatusCode != 200)
        {
            TempData["error"] = result;
            return RedirectToPage();
        }

        TempData["success"] = "Rating Successfully!";
        return RedirectToPage();
    }
}