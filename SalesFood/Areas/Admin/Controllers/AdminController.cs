using Microsoft.AspNetCore.Mvc;

namespace SalesFood.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}