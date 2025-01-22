using Microsoft.EntityFrameworkCore;
using SalesFood.Context;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;

namespace SalesFood.Repositories;

public class FoodRepository : IFoodRepository
{
    private readonly AppDbContext _context;

    public FoodRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Food> Foods => _context.Foods.Include(c => c.Category);

    public IEnumerable<Food> FavoriteFoods => _context.Foods.Where(p => p.IsFavoriteFood).Include(c => c.IsFavoriteFood);

    public Food GetFoodById(int id) => _context.Foods.FirstOrDefault(f => f.FoodId == id);
}

