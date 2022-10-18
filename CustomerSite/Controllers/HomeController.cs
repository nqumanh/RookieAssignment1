using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomerSite.Models;

namespace CustomerSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7012/");
        var response = await client.GetAsync("Product/GetAllProducts");
        var result = response.Content.ReadAsStringAsync().Result;
        var products = JsonConvert.DeserializeObject<List<Product>>(result);

        response = await client.GetAsync("Category/GetAllCategories");
        result = response.Content.ReadAsStringAsync().Result;
        var categories = JsonConvert.DeserializeObject<List<Category>>(result);

        var tupleModel = new Tuple<List<Category>?, List<Product>?>(categories, products);

        return View(tupleModel);
    }

    public IActionResult AboutUs()
    {
        return View();
    }

    public async Task<IActionResult> Shop(string bookCategory, string searchString)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7012/");
        var response = await client.GetAsync("Product/GetAllProducts");
        var result = response.Content.ReadAsStringAsync().Result;
        var products = JsonConvert.DeserializeObject<List<Product>>(result);

        response = await client.GetAsync("Category/GetAllCategories");
        result = response.Content.ReadAsStringAsync().Result;
        var categories = JsonConvert.DeserializeObject<List<Category>>(result);

        if (!string.IsNullOrEmpty(searchString))
        {
            products = products.Where(s => s.Name!.Contains(searchString)).ToList();
        }

        if (!string.IsNullOrEmpty(bookCategory))
        {
            products = products.Where(x => x.Categories.Any(category => category.Name == bookCategory)).ToList();
        }

        var bookCategoryVM = new BookCategoryViewModel
        {
            Categories = new SelectList(categories.Select(Category => Category.Name).ToList()),
            Products = products.ToList()
        };

        return View(bookCategoryVM);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult ContactUs()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
