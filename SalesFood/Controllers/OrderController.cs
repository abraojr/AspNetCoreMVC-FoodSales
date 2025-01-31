using Microsoft.AspNetCore.Mvc;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;

namespace SalesFood.Controllers;

public class OrderController : Controller
{

    private readonly IOrderRepository _orderRepository;
    private readonly ShoppingCart _shoppingCart;

    public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
    {
        _orderRepository = orderRepository;
        _shoppingCart = shoppingCart;
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Order order)
    {
        int totalOrderitems = 0;
        decimal totalPrice = 0.0m;

        // Gets the items from the customer's shopping cart
        List<ShoppingCartItem> items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;

        // Checks for order items
        if (_shoppingCart.ShoppingCartItems.Count == 0)
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
            _orderRepository.CreateOrder(order);

            // Set messages to the client
            ViewBag.CompleteCheckoutMessage = "Thanks for your order :)";
            ViewBag.TotalOrder = _shoppingCart.GetShoppingCartTotal();

            // Clear the customer's shopping cart
            _shoppingCart.ClearShoppinCart();

            // Displays the view with customer and order data
            return View("~/Views/Order/CompleteCheckout.cshtml", order);
        }

        return View(order);
    }
}
