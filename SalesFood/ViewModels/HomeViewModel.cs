using SalesFood.Models;

namespace SalesFood.ViewModels;

public class HomeViewModel
{
    public IEnumerable<Food> FavoriteFoods { get; set; }
}

