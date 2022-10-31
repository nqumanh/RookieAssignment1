using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CustomerSite.Helper;
using CustomerSite.Pages.Models;
using System.Text;

namespace CustomerSite.Pages;

public class OrderModel : PageModel
{
    private APIHelper _api = new APIHelper();

    private readonly ILogger<OrderModel> _logger;

    public OrderModel(ILogger<OrderModel> logger)
    {
        _logger = logger;
    }

    public decimal TotalPrice = 0;
    public List<CartItem> Cart = new List<CartItem>();
    public async Task<IActionResult> OnGetAddToCartAsync(int id)
    {
        Cart = GetCartItems();
        HttpClient client = _api.initial();
        var response = await client.GetAsync($"Product/GetProductById/{id}");
        var result = response.Content.ReadAsStringAsync().Result;
        var cartItem = JsonConvert.DeserializeObject<CartItem>(result) ?? new CartItem();
        var index = Cart.FindIndex(x => x.Id == cartItem.Id);
        if (index == -1)
        {
            cartItem.Quantity = 1;
            Cart.Add(cartItem);
        }
        else
        {
            Cart[index].Quantity++;
        }
        var session = HttpContext.Session;
        string jsoncart = JsonConvert.SerializeObject(Cart);
        session.SetString("cart", jsoncart);
        return RedirectToPage();
    }

    public void OnGet(int id)
    {
        Cart = GetCartItems();
        foreach (var item in Cart)
        {
            TotalPrice += item.Quantity * item.Price;
        }
    }

    public void OnGetClearCart(int id)
    {
        var session = HttpContext.Session;
        session.Remove("cart");
        Cart = new List<CartItem>();
    }

    public IActionResult OnGetCheckOut(int id)
    {
        var session = HttpContext.Session;
        session.Remove("cart");
        Cart = new List<CartItem>();
        return RedirectToPage("./Success");
    }

    public List<CartItem> GetCartItems()
    {
        var session = HttpContext.Session;
        string jsoncart = session.GetString("cart")!;
        if (jsoncart != null)
        {
            return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart) ?? new List<CartItem>();
        }
        return new List<CartItem>();
    }
}

