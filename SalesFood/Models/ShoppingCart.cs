using Microsoft.EntityFrameworkCore;
using SalesFood.Context;

namespace SalesFood.Models;

public class ShoppingCart
{
    private readonly AppDbContext _context;

    public ShoppingCart(AppDbContext context)
    {
        _context = context;
    }

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
        var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault
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

            _context.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Quantity++;
        }

        _context.SaveChanges();
    }

    public void RemoveFromShoppingCart(Food food)
    {
        var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault
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
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }

        _context.SaveChanges();
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ?? (ShoppingCartItems =
                                     _context.ShoppingCartItems
                                    .Where(c => c.ShoppingCartId == ShoppingCartId)
                                    .Include(s => s.Food)
                                    .ToList());
    }

    public void ClearShoppinCart()
    {
        var shoppingCartItems = _context.ShoppingCartItems
                                .Where(shoppingCart => shoppingCart.ShoppingCartId == ShoppingCartId);

        _context.ShoppingCartItems.RemoveRange(shoppingCartItems);

        _context.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        var total = _context.ShoppingCartItems
                    .Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Select(c => c.Food.Price * c.Quantity).Sum();

        return total;
    }
}

