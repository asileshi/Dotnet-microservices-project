using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers;

public class CartAPIController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}