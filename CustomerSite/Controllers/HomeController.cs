using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CustomerSite.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace CustomerSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = new HttpClient();
		client.BaseAddress = new Uri("https://localhost:7012/");
        var response = await client.GetAsync("Product/GetAllProducts");
        var result =  response.Content.ReadAsStringAsync().Result;
        var productList = JsonConvert.DeserializeObject<List<Product>>(result);

        response = await client.GetAsync("Category/GetAllCategories");
        result =  response.Content.ReadAsStringAsync().Result;
        var categoryList = JsonConvert.DeserializeObject<List<Category>>(result);

        var tupleModel = new Tuple<List<Category>, List<Product>>(categoryList, productList);

        return View(tupleModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
