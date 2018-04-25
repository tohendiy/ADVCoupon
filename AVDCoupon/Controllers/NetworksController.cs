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
using ADVCoupon.ViewModel.NetworkViewModels;

namespace ADVCoupon.Controllers
{
    public class NetworksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INetworkService _service;

        public NetworksController(ApplicationDbContext context, INetworkService service)
        {
            _service = service;
            _context = context;
        }

        // GET: Networks
        public async Task<IActionResult> Index()
        {
            var networksModel = await _service.GetNetworkViewModelsAsync();
            return View(networksModel);        }

        // GET: Networks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkModel = await _service.GetNetworkItemViewModelAsync(id.Value);
            if (networkModel == null)
            {
                return NotFound();
            }
            return View(networkModel);
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
        public async Task<IActionResult> Create(NetworkItemViewModel networkModel)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateNetworkAsync(networkModel);
                return RedirectToAction(nameof(Index));
            }
            return View(networkModel);
        }

        // GET: Networks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkModel = await _service.GetNetworkItemViewModelAsync(id.Value);
            if (networkModel == null)
            {
                return NotFound();
            }
            return View(networkModel);
        }

        // POST: Networks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, NetworkItemViewModel networkModel)
        {
            if (id != networkModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateNetworkAsync(networkModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetworkExists(networkModel.Id))
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
            return View(networkModel);
        }

        // GET: Networks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkModel = await _service.GetNetworkItemViewModelAsync(id.Value);
            if (networkModel == null)
            {
                return NotFound();
            }

            return View(networkModel);
        }

        // POST: Networks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteNetworkAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool NetworkExists(Guid id)
        {
            return _service.IsExist(id);
        }
    }
}
