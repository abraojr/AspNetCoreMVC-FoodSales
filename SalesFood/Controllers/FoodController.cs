using Microsoft.AspNetCore.Mvc;
using SalesFood.Models;
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

    public IActionResult List(string category)
    {
        IEnumerable<Food> foods;
        string currentCategory = string.Empty;

        if (string.IsNullOrEmpty(category))
        {
            foods = _foodRepository.Foods.OrderBy(x => x.FoodId);
            currentCategory = "All Products";
        }
        else
        {
            foods = _foodRepository.Foods
                           .Where(x => x.Category.Name.Equals(category))
                           .OrderBy(x => x.Name);

            currentCategory = category;
        }

        var foodListViewModel = new FoodListViewModel
        {
            Foods = foods,
            CurrentCategory = currentCategory
        };

        return View(foodListViewModel);
    }

    public IActionResult Details(int foodId)
    {
        var food = _foodRepository.Foods.FirstOrDefault(x => x.FoodId == foodId);

        return View(food);
    }
}
