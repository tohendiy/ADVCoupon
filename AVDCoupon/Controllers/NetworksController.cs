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
    public class NetworksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NetworksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Networks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Networks.ToListAsync());
        }

        // GET: Networks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var network = await _context.Networks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (network == null)
            {
                return NotFound();
            }

            return View(network);
        }

        // GET: Networks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Networks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Caption,LogoImage")] Network network)
        {
            if (ModelState.IsValid)
            {
                network.Id = Guid.NewGuid();
                _context.Add(network);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(network);
        }

        // GET: Networks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var network = await _context.Networks.SingleOrDefaultAsync(m => m.Id == id);
            if (network == null)
            {
                return NotFound();
            }
            return View(network);
        }

        // POST: Networks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Caption,LogoImage")] Network network)
        {
            if (id != network.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(network);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetworkExists(network.Id))
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
            return View(network);
        }

        // GET: Networks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var network = await _context.Networks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (network == null)
            {
                return NotFound();
            }

            return View(network);
        }

        // POST: Networks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var network = await _context.Networks.SingleOrDefaultAsync(m => m.Id == id);
            _context.Networks.Remove(network);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NetworkExists(Guid id)
        {
            return _context.Networks.Any(e => e.Id == id);
        }
    }
}
