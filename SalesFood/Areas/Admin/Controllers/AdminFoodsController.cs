using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesFood.Context;
using Microsoft.AspNetCore.Authorization;
using SalesFood.Models;
using ReflectionIT.Mvc.Paging;

namespace SalesFood.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminFoodsController : Controller
{
    private readonly AppDbContext _context;

    public AdminFoodsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Admin/AdminFoods
    public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Name")
    {
        var result = _context.Foods.Include(l => l.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            result = result.Where(p => p.Name.Contains(filter));
        }

        var model = await PagingList.CreateAsync(result, 5, pageindex, sort, "Name");

        model.RouteValue = new RouteValueDictionary { { "filter", filter } };

        return View(model);
    }

    // GET: Admin/AdminFoods/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var food = await _context.Foods
            .Include(l => l.Category)
            .FirstOrDefaultAsync(m => m.FoodId == id);

        if (food == null)
        {
            return NotFound();
        }

        return View(food);
    }

    // GET: Admin/AdminFoods/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
        return View();
    }

    // POST: Admin/AdminFoods/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FoodId,Name,ShortDescription,LongDescription,Price,ImageUrl,ImageThumbnailUrl,IsFavoriteFood,InStock,CategoryId")] Food food)
    {
        if (ModelState.IsValid)
        {
            _context.Add(food);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", food.CategoryId);
        return View(food);
    }

    // GET: Admin/AdminFoods/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var food = await _context.Foods.FindAsync(id);

        if (food == null)
        {
            return NotFound();
        }

        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", food.CategoryId);
        return View(food);
    }

    // POST: Admin/AdminFoods/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("FoodId,Name,ShortDescription,LongDescription,Price,ImageUrl,ImageThumbnailUrl,IsFavoriteFood,InStock,CategoryId")] Food food)
    {
        if (id != food.FoodId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(food);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(food.FoodId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", food.CategoryId);
        return View(food);
    }

    // GET: Admin/AdminFoods/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var food = await _context.Foods
            .Include(l => l.Category)
            .FirstOrDefaultAsync(m => m.FoodId == id);

        if (food == null)
        {
            return NotFound();
        }

        return View(food);
    }

    // POST: Admin/AdminFoods/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var food = await _context.Foods.FindAsync(id);

        _context.Foods.Remove(food);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    private bool FoodExists(int id)
    {
        return _context.Foods.Any(e => e.FoodId == id);
    }
}