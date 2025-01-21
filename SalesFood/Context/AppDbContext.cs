using Microsoft.EntityFrameworkCore;
using SalesFood.Models;

namespace SalesFood.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Food> Foods { get; set; }
}

