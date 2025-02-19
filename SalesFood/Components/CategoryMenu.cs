using Microsoft.AspNetCore.Mvc;
using SalesFood.Repositories.Interfaces;

namespace SalesFood.Components;

public class CategoryMenu(ICategoryRepository categoryRepository) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var categories = categoryRepository.Categories.OrderBy(c => c.Name);
        return View(categories);
    }
}