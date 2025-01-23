using Microsoft.AspNetCore.Mvc;
using SalesFood.Repositories.Interfaces;
using SalesFood.ViewModels;

namespace SalesFood.Controllers;

public class FoodController : Controller
{
    private readonly IFoodRepository _foodRepository;

    public FoodController(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public IActionResult List()
    {
        var foodListViewModel = new FoodListViewModel();
        foodListViewModel.Foods = _foodRepository.Foods;
        foodListViewModel.CurrentCategory = "Current Category";

        return View(foodListViewModel);
    }
}

