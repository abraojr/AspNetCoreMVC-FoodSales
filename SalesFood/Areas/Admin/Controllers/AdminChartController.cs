using Microsoft.AspNetCore.Mvc;
using SalesFood.Areas.Admin.Services;

namespace SalesFood.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminChartController(SalesChartService salesChart) : Controller
{
    private readonly SalesChartService _salesChart = salesChart ?? throw new ArgumentNullException(nameof(salesChart));

    public JsonResult ProductSales(int days)
    {
        var productTotalSales = _salesChart.GetProductSales(days);
        return Json(productTotalSales);
    }

    public IActionResult Index(int days)
    {
        return View();
    }

    public IActionResult MonthlySales(int days)
    {
        return View();
    }

    public IActionResult WeeklySales(int days)
    {
        return View();
    }
}