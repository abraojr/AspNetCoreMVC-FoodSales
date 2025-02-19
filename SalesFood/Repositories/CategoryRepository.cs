using SalesFood.Context;
using SalesFood.Models;
using SalesFood.Repositories.Interfaces;

namespace SalesFood.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public IEnumerable<Category> Categories => context.Categories;
}