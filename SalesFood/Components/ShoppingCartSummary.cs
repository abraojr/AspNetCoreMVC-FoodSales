using Microsoft.AspNetCore.Mvc;
using SalesFood.Models;
using SalesFood.ViewModels;

namespace SalesFood.Components;

public class ShoppingCartSummary(ShoppingCart shoppingCart) : ViewComponent
{
    public IViewComponentResult Invoke()
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
}