using SalesFood.Context;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;

namespace SalesFood.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> Categories => _context.Categories;
}

