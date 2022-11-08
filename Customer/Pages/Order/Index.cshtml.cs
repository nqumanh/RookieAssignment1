using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using CustomerSite.Helper;
using CustomerSite.Pages.Models;
using System.Text;
using SharedViewModels;

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
    [BindProperty]
    public OrderDTO OrderForm { get; set; } = new OrderDTO();
    public async Task<IActionResult> OnGetAddToCartAsync(int id)
    {
        Cart = GetCartItems();
        HttpClient client = _api.initial();
        var response = await client.GetAsync($"Product/Read/{id}");
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

    public async Task<IActionResult> OnPostAsync()
    {
        OrderForm.UserId = HttpContext!.Request.Cookies["Id"]!;

        var token = HttpContext.Request.Cookies["AccessToken"];
        if (token == null)
        {
            TempData["error"] = "You have to login first!";
            return RedirectToPage();
        }

        Cart = GetCartItems();

        if (Cart.Count == 0)
        {
            TempData["error"] = "Your cart is empty!";
            return RedirectToPage();
        }

        HttpClient client = _api.initial();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        OrderForm.Cart = Cart.Select(x => new CartItemDTO { ProductId = x.Id, Quantity = x.Quantity }).ToList();
        var response = await client.PostAsJsonAsync("User/Order", OrderForm);
        var result = response.Content.ReadAsStringAsync().Result;

        if ((int)response.StatusCode != 200)
        {
            TempData["error"] = result;
            return RedirectToPage();
        }

        var session = HttpContext.Session;
        session.Remove("cart");
        Cart = new List<CartItem>();
        TempData["success"] = "Order Successfully";
        return RedirectToPage("/Index");
    }

    public IActionResult OnPostUpdateOrderLine(int id, int quantity)
    {
        Cart = GetCartItems();

        var item = Cart.Find(s => s.Id == id);
        if (item == null)
            return RedirectToPage();
        if (quantity < 0)
        {
            TempData["error"] = "Quantity cannot be a negative number!";
            return RedirectToPage();
        }
        else if (quantity == 0)
            Cart = Cart.Where(x => x.Id != id).ToList();
        else
        {
            item.Quantity = quantity;
        }

        var session = HttpContext.Session;
        string jsoncart = JsonConvert.SerializeObject(Cart);
        session.SetString("cart", jsoncart);
        return RedirectToPage();
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

