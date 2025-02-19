using Microsoft.AspNetCore.Mvc;

namespace SalesFood.Controllers;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}