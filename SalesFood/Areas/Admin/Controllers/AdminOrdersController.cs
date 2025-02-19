using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SalesFood.Context;
using SalesFood.Models;
using SalesFood.ViewModels;

namespace SalesFood.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController(AppDbContext context) : Controller
    {

        // GET: Admin/AdminOrders
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Name")
        {
            var result = context.Orders.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                result = result.Where(p => p.Name.Contains(filter));
            }

            var model = await PagingList.CreateAsync(result, 5, pageindex, sort, "Name");

            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        // GET: Admin/AdminOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/AdminOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Name,LastName,Address,AddressComplement,ZipCode,State,City,PhoneNumber,Email,OrderDate,DeliveryDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                context.Add(order);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Admin/AdminOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Admin/AdminOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Name,LastName,Address,AddressComplement,ZipCode,State,City,PhoneNumber,Email,OrderDate,DeliveryDate")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(order);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            return View(order);
        }

        // GET: Admin/AdminOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await context.Orders.FindAsync(id);

            context.Orders.Remove(order);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult OrderFoods(int? id)
        {
            var order = context.Orders
                            .Include(o => o.OrderItems)
                            .ThenInclude(f => f.Food)
                            .FirstOrDefault(p => p.OrderId == id);

            if (order == null)
            {
                Response.StatusCode = 404;
                return View("OrderNotFound", id.Value);
            }

            OrderFoodViewModel orderFoods = new()
            {
                Order = order,
                OrderDetails = order.OrderItems
            };

            return View(orderFoods);
        }

        private bool OrderExists(int id)
        {
            return context.Orders.Any(e => e.OrderId == id);
        }
    }
}