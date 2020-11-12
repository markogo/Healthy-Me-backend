#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles= "Admin")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Orders.Include(o => o.AppUser).Include(o => o.OrderType).Include(o => o.Restaurant);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.OrderType)
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["DrinkId"] = new SelectList(_context.Drinks, "Id", "Id");
            ViewData["FoodId"] = new SelectList(_context.Foods, "Id", "Id");
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Id");
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Id");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderStatus,Number,TimeCreated,FoodId,IngredientId,DrinkId,RestaurantId,OrderTypeId,AppUserId,PersonId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", order.AppUserId);
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Id", order.OrderTypeId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", order.RestaurantId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", order.AppUserId);
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Id", order.OrderTypeId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", order.RestaurantId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderStatus,Number,TimeCreated,FoodId,IngredientId,DrinkId,RestaurantId,OrderTypeId,AppUserId,PersonId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", order.AppUserId);
            ViewData["OrderTypeId"] = new SelectList(_context.OrderTypes, "Id", "Id", order.OrderTypeId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "Id", "Id", order.RestaurantId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.AppUser)
                .Include(o => o.OrderType)
                .Include(o => o.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)   {
     
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

    }
}
