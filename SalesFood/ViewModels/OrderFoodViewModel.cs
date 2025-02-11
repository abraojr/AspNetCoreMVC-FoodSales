using SalesFood.Models;

namespace SalesFood.ViewModels;

public class OrderFoodViewModel
{
    public Order Order { get; set; }
    public IEnumerable<OrderDetail> OrderDetails { get; set; }
}