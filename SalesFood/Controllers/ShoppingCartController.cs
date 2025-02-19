using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;
using SalesFood.ViewModels;

namespace SalesFood.Controllers;

public class ShoppingCartController(IFoodRepository foodRepository, ShoppingCart shoppingCart) : Controller
{
    public IActionResult Index()
    {
        var items = shoppingCart.GetShoppingCartItems();

        shoppingCart.ShoppingCartItems = items;

        var shoppingCartViewModel = new ShoppingCartViewModel
        {
            ShoppingCart = shoppingCart,
            ShoppingCartTotal = shoppingCart.GetShoppingCartTotal()
        };

        return View(shoppingCartViewModel);
    }

    [Authorize]
    public IActionResult AddItemToShoppingCart(int foodId)
    {
        var selectedFood = foodRepository.Foods.FirstOrDefault(p => p.FoodId == foodId);

        if (selectedFood != null)
        {
            shoppingCart.AddToShoppingCart(selectedFood);
        }

        return RedirectToAction("Index");
    }

    [Authorize]
    public IActionResult RemoveItemFromShoppingCart(int foodId)
    {
        var selectedFood = foodRepository.Foods.FirstOrDefault(p => p.FoodId == foodId);

        if (selectedFood != null)
        {
            shoppingCart.RemoveFromShoppingCart(selectedFood);
        }

        return RedirectToAction("Index");
    }
}