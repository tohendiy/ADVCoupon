using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADVCoupon.Models;
using AVDCoupon.Data;

namespace ADVCoupon.Controllers
{
    public class NetworkPointsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NetworkPointsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NetworkPoints
        public async Task<IActionResult> Index()
        {
            return View(await _context.NetworkPoints.ToListAsync());
        }

        // GET: NetworkPoints/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkPoint = await _context.NetworkPoints
                .SingleOrDefaultAsync(m => m.Id == id);
            if (networkPoint == null)
            {
                return NotFound();
            }

            return View(networkPoint);
        }

        // GET: NetworkPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NetworkPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] NetworkPoint networkPoint)
        {
            if (ModelState.IsValid)
            {
                networkPoint.Id = Guid.NewGuid();
                _context.Add(networkPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(networkPoint);
        }

        // GET: NetworkPoints/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkPoint = await _context.NetworkPoints.SingleOrDefaultAsync(m => m.Id == id);
            if (networkPoint == null)
            {
                return NotFound();
            }
            return View(networkPoint);
        }

        // POST: NetworkPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] NetworkPoint networkPoint)
        {
            if (id != networkPoint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(networkPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetworkPointExists(networkPoint.Id))
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
            return View(networkPoint);
        }

        // GET: NetworkPoints/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkPoint = await _context.NetworkPoints
                .SingleOrDefaultAsync(m => m.Id == id);
            if (networkPoint == null)
            {
                return NotFound();
            }

            return View(networkPoint);
        }

        // POST: NetworkPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var networkPoint = await _context.NetworkPoints.SingleOrDefaultAsync(m => m.Id == id);
            _context.NetworkPoints.Remove(networkPoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NetworkPointExists(Guid id)
        {
            return _context.NetworkPoints.Any(e => e.Id == id);
        }
    }
}
