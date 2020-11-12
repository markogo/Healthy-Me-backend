#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles= "Admin")]
    public class OrderTypesController : Controller
    {
        private readonly AppDbContext _context;

        public OrderTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderTypes.ToListAsync());
        }

        // GET: OrderTypes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderType = await _context.OrderTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderType == null)
            {
                return NotFound();
            }

            return View(orderType);
        }

        // GET: OrderTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Comment,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] OrderType orderType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderType);
        }

        // GET: OrderTypes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderType = await _context.OrderTypes.FindAsync(id);
            if (orderType == null)
            {
                return NotFound();
            }
            return View(orderType);
        }

        // POST: OrderTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Comment,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] OrderType orderType)
        {
            if (id != orderType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderTypeExists(orderType.Id))
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
            return View(orderType);
        }

        // GET: OrderTypes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderType = await _context.OrderTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderType == null)
            {
                return NotFound();
            }

            return View(orderType);
        }

        // POST: OrderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderType = await _context.OrderTypes.FindAsync(id);
            _context.OrderTypes.Remove(orderType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderTypeExists(Guid id)
        {
            return _context.OrderTypes.Any(e => e.Id == id);
        }

    }
}
