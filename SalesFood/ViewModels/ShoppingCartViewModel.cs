using SalesFood.Models;

namespace SalesFood.ViewModels;

public class ShoppingCartViewModel
{
    public ShoppingCart ShoppingCart { get; set; }
    public decimal ShoppingCartTotal { get; set; }
}