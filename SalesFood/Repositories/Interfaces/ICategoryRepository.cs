using SalesFood.Models;

namespace SalesFood.Repositories.Interfaces;

public interface ICategoryRepository
{
    IEnumerable<Category> Categories { get; }
}
