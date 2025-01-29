using SalesFood.Context;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;

namespace SalesFood.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ShoppingCart _shoppingCart;

    public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
    {
        _appDbContext = appDbContext;
        _shoppingCart = shoppingCart;
    }

    public void CreateOrder(Order order)
    {
        order.OrderDate = DateTime.Now;
        _appDbContext.Orders.Add(order);
        _appDbContext.SaveChanges();

        var shoppingCartItems = _shoppingCart.ShoppingCartItems;

        foreach (var cartItem in shoppingCartItems)
        {
            var orderDetail = new OrderDetail
            {
                Quantity = cartItem.Quantity,
                FoodId = cartItem.Food.FoodId,
                OrderId = order.OrderId,
                Price = cartItem.Food.Price
            };

            _appDbContext.OrderDetails.Add(orderDetail);
        }

        _appDbContext.SaveChanges();
    }
}