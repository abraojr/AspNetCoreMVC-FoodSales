using Microsoft.EntityFrameworkCore;
using SalesFood.Context;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;

namespace SalesFood.Repositories;

public class FoodRepository(AppDbContext context) : IFoodRepository
{
    public IEnumerable<Food> Foods => context.Foods.Include(c => c.Category);

    public IEnumerable<Food> FavoriteFoods => context.Foods.Where(p => p.IsFavoriteFood).Include(c => c.Category);

    public Food GetFoodById(int id) => context.Foods.FirstOrDefault(f => f.FoodId == id);
}