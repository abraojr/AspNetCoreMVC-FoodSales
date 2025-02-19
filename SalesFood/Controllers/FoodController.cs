using Microsoft.AspNetCore.Mvc;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;
using SalesFood.ViewModels;

namespace SalesFood.Controllers;

public class FoodController(IFoodRepository foodRepository) : Controller
{
    public IActionResult List(string category)
    {
        IEnumerable<Food> foods;
        string currentCategory = string.Empty;

        if (string.IsNullOrEmpty(category))
        {
            foods = foodRepository.Foods.OrderBy(x => x.FoodId);
            currentCategory = "All Products";
        }
        else
        {
            foods = foodRepository.Foods
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
        var food = foodRepository.Foods.FirstOrDefault(x => x.FoodId == foodId);

        return View(food);
    }

    public ViewResult Search(string searchString)
    {
        IEnumerable<Food> foods;
        string currentCategory = string.Empty;

        if (string.IsNullOrEmpty(searchString))
        {
            foods = foodRepository.Foods.OrderBy(x => x.FoodId);
            currentCategory = "All Food";
        }
        else
        {
            foods = foodRepository.Foods
                        .Where(x => x.Name.ToLower().Contains(searchString.ToLower()));

            if (foods.Any())
            {
                currentCategory = "Food";
            }
            else
            {
                currentCategory = "Product not found";
            }
        }

        return View("~/Views/Food/List.cshtml", new FoodListViewModel
        {
            Foods = foods,
            CurrentCategory = currentCategory
        });
    }
}