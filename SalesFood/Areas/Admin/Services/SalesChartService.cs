using SalesFood.Context;
using SalesFood.Models;

namespace SalesFood.Areas.Admin.Services;

public class SalesChartService(AppDbContext context)
{
    public List<ProductChart> GetProductSales(int days = 360)
    {
        var date = DateTime.Now.AddDays(-days);

        var products = (from od in context.OrderDetails
                        join f in context.Foods on od.FoodId equals f.FoodId
                        where od.Order.OrderDate >= date
                        group od by new { od.FoodId, f.Name }
                       into g
                        select new ProductChart
                        {
                            ProductName = g.Key.Name,
                            ProductQuantity = g.Sum(q => q.Quantity),
                            ProductTotalValue = g.Sum(a => a.Price * a.Quantity),
                        }).ToList();

        return products;
    }
}