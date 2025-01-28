﻿using Microsoft.AspNetCore.Mvc;
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
            currentCategory = "All Food";
        }
        else
        {
            if (string.Equals("Normal", category, StringComparison.OrdinalIgnoreCase))
            {
                foods = _foodRepository.Foods
                            .Where(x => x.Category.Name.Equals("Normal"))
                            .OrderBy(x => x.Name);
            }
            else
            {
                foods = _foodRepository.Foods
                           .Where(x => x.Category.Name.Equals("Natural"))
                           .OrderBy(x => x.Name);
            }

            currentCategory = category;
        }

        var foodListViewModel = new FoodListViewModel
        {
            Foods = foods,
            CurrentCategory = currentCategory
        };

        return View(foodListViewModel);
    }
}
