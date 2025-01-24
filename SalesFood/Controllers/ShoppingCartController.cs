using Microsoft.AspNetCore.Mvc;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;
using SalesFood.ViewModels;

namespace SalesFood.Controllers;

public class ShoppingCartController : Controller
{
    private readonly IFoodRepository _foodRepository;
    private readonly ShoppingCart _shoppingCart;

    public ShoppingCartController(IFoodRepository foodRepository, ShoppingCart shoppingCart)
    {
        _foodRepository = foodRepository;
        _shoppingCart = shoppingCart;
    }

    public IActionResult Index()
    {
        var items = _shoppingCart.GetShoppingCartItems();

        _shoppingCart.ShoppingCartItems = items;

        var shoppingCartViewModel = new ShoppingCartViewModel
        {
            ShoppingCart = _shoppingCart,
            ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
        };

        return View(shoppingCartViewModel);
    }

    public IActionResult AddItemToShoppingCart(int foodId)
    {
        var selectedFood = _foodRepository.Foods.FirstOrDefault(p => p.FoodId == foodId);

        if (selectedFood != null)
        {
            _shoppingCart.AddToShoppingCart(selectedFood);
        }

        return RedirectToAction("Index");
    }

    public IActionResult RemoveItemFromShoppingCart(int foodId)
    {
        var selectedFood = _foodRepository.Foods.FirstOrDefault(p => p.FoodId == foodId);

        if (selectedFood != null)
        {
            _shoppingCart.RemoveFromShoppingCart(selectedFood);
        }

        return RedirectToAction("Index");
    }
}

