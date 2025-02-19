using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;

namespace SalesFood.Controllers;

public class OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart) : Controller
{
    [HttpGet]
    [Authorize]
    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public IActionResult Checkout(Order order)
    {
        int totalOrderitems = 0;
        decimal totalPrice = 0.0m;

        // Gets the items from the customer's shopping cart
        List<ShoppingCartItem> items = shoppingCart.GetShoppingCartItems();
        shoppingCart.ShoppingCartItems = items;

        // Checks for order items
        if (shoppingCart.ShoppingCartItems.Count == 0)
        {
            ModelState.AddModelError("", "Your shopping cart is empty");
        }

        // Calculate the total number of items and the total price of the order
        foreach (var item in items)
        {
            totalOrderitems += item.Quantity;
            totalPrice += item.Food.Price * item.Quantity;
        }

        // Set the values obtained to the order
        order.TotalOrderItems = totalOrderitems;
        order.TotalOrder = totalPrice;

        // Validate the order data
        if (ModelState.IsValid)
        {
            // Create the order and details
            orderRepository.CreateOrder(order);

            // Set messages to the client
            ViewBag.CompleteCheckoutMessage = "Thanks for your order :)";
            ViewBag.TotalOrder = shoppingCart.GetShoppingCartTotal();

            // Clear the customer's shopping cart
            shoppingCart.ClearShoppinCart();

            // Displays the view with customer and order data
            return View("~/Views/Order/CompleteCheckout.cshtml", order);
        }

        return View(order);
    }
}