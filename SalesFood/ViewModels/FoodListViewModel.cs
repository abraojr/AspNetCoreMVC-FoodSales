using SalesFood.Models;

namespace SalesFood.ViewModels;

public class FoodListViewModel
{
    public IEnumerable<Food> Foods { get; set; }
    public string CurrentCategory { get; set; }
}

