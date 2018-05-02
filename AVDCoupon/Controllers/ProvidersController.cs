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
using ADVCoupon.ViewModel.ProviderViewModels;
using Microsoft.AspNetCore.Authorization;
using ADVCoupon.Helpers;

namespace ADVCoupon.Controllers
{
    [Authorize(Roles = Constants.ADMIN_ROLE)]
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
            var providersModel = await _service.GetProviderViewModelsAsync();
            return View(providersModel);
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerModel = await _service.GetProviderItemViewModelAsync(id.Value);
            if(providerModel == null)
            {
                return NotFound();
            }
            return View(providerModel);
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
        public async Task<IActionResult> Create(ProviderItemViewModel providerModel)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateProviderAsync(providerModel);
                return RedirectToAction(nameof(Index));
            }
            return View(providerModel);
        }

        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerModel = await _service.GetProviderItemViewModelAsync(id.Value);
            if (providerModel == null)
            {
                return NotFound();
            }
            return View(providerModel);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProviderItemViewModel providerModel)
        {
            if (id != providerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateProviderAsync(providerModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(providerModel.Id))
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
            return View(providerModel);
        }

        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providerModel = await _service.GetProviderItemViewModelAsync(id.Value);
            if (providerModel == null)
            {
                return NotFound();
            }

            return View(providerModel);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteProviderAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<PartialViewResult> IndexGrid()
        {
            var providersModel = await _service.GetProviderViewModelsAsync();

            return PartialView("_IndexGrid", providersModel);
        }
        private bool ProviderExists(Guid id)
        {
            return _service.IsExist(id);
        }
    }
}
