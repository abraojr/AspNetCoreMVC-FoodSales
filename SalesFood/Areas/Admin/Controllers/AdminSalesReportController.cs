﻿using Microsoft.AspNetCore.Mvc;
using SalesFood.Areas.Admin.Services;

namespace SalesFood.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminSalesReportController(SalesReportService salesReportService) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> SalesReportSimple(DateTime? minDate, DateTime? maxDate)
    {
        if (!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year, 1, 1);
        }

        if (!maxDate.HasValue)
        {
            maxDate = DateTime.Now;
        }

        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

        var result = await salesReportService.FindByDateAsync(minDate, maxDate);
        return View(result);
    }
}