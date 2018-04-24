using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADVCoupon.Models;
using AVDCoupon.Data;
using ADVCoupon.Services;

namespace ADVCoupon.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProviderService _service;

        public ProvidersController(ApplicationDbContext context, IProviderService service)
        {
            _service = service;
            _context = context;
        }

        // GET: Providers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Providers.ToListAsync());
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // GET: Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LogoImage")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                provider.Id = Guid.NewGuid();
                _context.Add(provider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provider);
        }

        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers.SingleOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }
            return View(provider);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,LogoImage")] Provider provider)
        {
            if (id != provider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(provider.Id))
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
            return View(provider);
        }

        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteProviderAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(Guid id)
        {
            return _service.IsExist(id);
        }
    }
}
