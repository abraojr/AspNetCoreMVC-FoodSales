using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesFood.Context;
using SalesFood.Models;

namespace SalesFood.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminCategoriesController(AppDbContext context) : Controller
{

    // GET: Admin/AdminCategories
    public async Task<IActionResult> Index()
    {
        return View(await context.Categories.ToListAsync());
    }

    // GET: Admin/AdminCategories/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // GET: Admin/AdminCategories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/AdminCategories/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CategoryId,Name,Description")] Category category)
    {
        if (ModelState.IsValid)
        {
            context.Add(category);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(category);
    }

    // GET: Admin/AdminCategories/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Admin/AdminCategories/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,Description")] Category category)
    {
        if (id != category.CategoryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(category);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
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

        return View(category);
    }

    // GET: Admin/AdminCategories/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    // POST: Admin/AdminCategories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await context.Categories.FindAsync(id);

        context.Categories.Remove(category);

        await context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    private bool CategoryExists(int id)
    {
        return context.Categories.Any(e => e.CategoryId == id);
    }
}