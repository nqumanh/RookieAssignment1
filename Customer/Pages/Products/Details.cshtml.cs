using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using CustomerSite.Helper;
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
    public string? Stars { get; set; }
    [BindProperty]
    public ReviewFormDTO ReviewForm { get; set; } = default!;
    public List<RatingDTO> RatingList { get; set; } = new List<RatingDTO>();

    public async Task OnGetAsync(int? id)
    {
        HttpClient client = _api.initial();
        var response = await client.GetAsync($"Product/GetProductById/{id}");
        var result = response.Content.ReadAsStringAsync().Result;
        Product = JsonConvert.DeserializeObject<ProductDTO>(result) ?? new ProductDTO();

        response = await client.GetAsync($"Product/GetAllRatingsOfProduct/{id}");
        result = response.Content.ReadAsStringAsync().Result;
        RatingList = JsonConvert.DeserializeObject<List<RatingDTO>>(result) ?? new List<RatingDTO>();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        HttpClient client = _api.initial();
        ReviewForm.ProductId = id;
        ReviewForm.UserId = 10;
        ReviewForm.Star = Int32.Parse(Stars);
        await client.PostAsJsonAsync("Product/WriteReview", ReviewForm);
        return RedirectToPage();
    }
}