using Microsoft.AspNetCore.Mvc;
using SalesFood.Repositories.Interfaces;

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
        var foods = _foodRepository.Foods;
        return View(foods);
    }
}

