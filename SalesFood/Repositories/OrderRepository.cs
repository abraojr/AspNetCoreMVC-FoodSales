using SalesFood.Context;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;

namespace SalesFood.Repositories;

public class OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart) : IOrderRepository
{
    public void CreateOrder(Order order)
    {
        order.OrderDate = DateTime.Now;
        appDbContext.Orders.Add(order);
        appDbContext.SaveChanges();

        var shoppingCartItems = shoppingCart.ShoppingCartItems;

        foreach (var cartItem in shoppingCartItems)
        {
            var orderDetail = new OrderDetail
            {
                Quantity = cartItem.Quantity,
                FoodId = cartItem.Food.FoodId,
                OrderId = order.OrderId,
                Price = cartItem.Food.Price
            };

            appDbContext.OrderDetails.Add(orderDetail);
        }

        appDbContext.SaveChanges();
    }
}