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
    public class FoodsController : Controller
    {
        private readonly AppDbContext _context;

        public FoodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Foods
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Foods.Include(f => f.FoodType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.FoodType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            ViewData["FoodTypeId"] = new SelectList(_context.FoodTypes, "Id", "Id");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Name,Amount,Size,FoodTypeId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodTypeId"] = new SelectList(_context.FoodTypes, "Id", "Id", food.FoodTypeId);
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(Guid id)
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
            ViewData["FoodTypeId"] = new SelectList(_context.FoodTypes, "Id", "Id", food.FoodTypeId);
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Description,Name,Amount,Size,FoodTypeId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Food food)
        {
            if (id != food.Id)
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
                    if (!FoodExists(food.Id))
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
            ViewData["FoodTypeId"] = new SelectList(_context.FoodTypes, "Id", "Id", food.FoodTypeId);
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .Include(f => f.FoodType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var food = await _context.Foods.FindAsync(id);
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(Guid id)
        {
            return _context.Foods.Any(e => e.Id == id);
        }

    }
}
