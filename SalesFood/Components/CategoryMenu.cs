using Microsoft.AspNetCore.Mvc;
using SalesFood.Repositories.Interfaces;

namespace SalesFood.Components;

public class CategoryMenu : ViewComponent
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryMenu(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _categoryRepository.Categories.OrderBy(c => c.Name);
        return View(categories);
    }
}

