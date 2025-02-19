using Microsoft.EntityFrameworkCore;
using SalesFood.Context;

namespace SalesFood.Models;

public class ShoppingCart(AppDbContext context)
{
    public string ShoppingCartId { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }

    public static ShoppingCart GetShoppingCart(IServiceProvider services)
    {
        // Defines a session
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        // Gets a service of the type in our context
        var context = services.GetService<AppDbContext>();

        // Gets or generates the shopping cart id
        string shoppingCartId = session.GetString("ShoppingCartId") ?? Guid.NewGuid().ToString();

        // Sets the shopping cart id in the session
        session.SetString("ShoppingCartId", shoppingCartId);

        // Returns the  shopping cart with the context and the id assigned or obtained
        return new ShoppingCart(context)
        {
            ShoppingCartId = shoppingCartId
        };
    }

    public void AddToShoppingCart(Food food)
    {
        var shoppingCartItem = context.ShoppingCartItems.SingleOrDefault
                                    (
                                        s => s.Food.FoodId == food.FoodId &&
                                        s.ShoppingCartId == ShoppingCartId
                                    );

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                Food = food,
                Quantity = 1
            };

            context.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Quantity++;
        }

        context.SaveChanges();
    }

    public void RemoveFromShoppingCart(Food food)
    {
        var shoppingCartItem = context.ShoppingCartItems.SingleOrDefault
                                    (
                                        s => s.Food.FoodId == food.FoodId &&
                                        s.ShoppingCartId == ShoppingCartId
                                    );

        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Quantity > 1)
            {
                shoppingCartItem.Quantity--;
            }
            else
            {
                context.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }

        context.SaveChanges();
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ?? (ShoppingCartItems =
                                     context.ShoppingCartItems
                                    .Where(c => c.ShoppingCartId == ShoppingCartId)
                                    .Include(s => s.Food)
                                    .ToList());
    }

    public void ClearShoppinCart()
    {
        var shoppingCartItems = context.ShoppingCartItems
                                .Where(shoppingCart => shoppingCart.ShoppingCartId == ShoppingCartId);

        context.ShoppingCartItems.RemoveRange(shoppingCartItems);

        context.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        var total = context.ShoppingCartItems
                    .Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Select(c => c.Food.Price * c.Quantity).Sum();

        return total;
    }
}