using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;
using SalesFood.ViewModels;

namespace SalesFood.Controllers;

public class HomeController(IFoodRepository foodRepository) : Controller
{
    public IActionResult Index()
    {
        var homeViewModel = new HomeViewModel
        {
            FavoriteFoods = foodRepository.FavoriteFoods
        };

        return View(homeViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}