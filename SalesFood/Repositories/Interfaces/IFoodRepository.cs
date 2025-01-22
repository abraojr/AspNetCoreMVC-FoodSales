using SalesFood.Models;

namespace SalesFood.Repositories.Interfaces;

public interface IFoodRepository
{
    IEnumerable<Food> Foods { get; }
    IEnumerable<Food> FavoriteFoods { get; }
    Food GetFoodById(int id);
}

