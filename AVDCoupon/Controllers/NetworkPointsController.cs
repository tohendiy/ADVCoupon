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
using ADVCoupon.ViewModel.NetworkPointViewModels;

namespace ADVCoupon.Controllers
{
    public class NetworkPointsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INetworkPointService _service;

        public NetworkPointsController(ApplicationDbContext context, INetworkPointService service)
        {
            _context = context;
            _service = service;
        }

        // GET: NetworkPoints
        public async Task<IActionResult> Index()
        {
            var networkPointModel = await _service.GetNetworkPointViewModelsAsync();
            return View(networkPointModel);
        }

        // GET: NetworkPoints/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkPointModel = await _service.GetNetworkPointViewModelAsync(id.Value);
            if (networkPointModel == null)
            {
                return NotFound();
            }
            return View(networkPointModel);
        }

        // GET: NetworkPoints/Create
        public async Task<IActionResult> Create()
        {
            var networksModel = await _service.GetNetworkPointNetworkListItemViewModelAsync();
            return View(networksModel);
        }

        // POST: NetworkPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NetworkPointViewModel networkPointModel)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateNetworkPointAsync(networkPointModel);

                return RedirectToAction(nameof(Index));
            }
            return View(networkPointModel);
        }

        // GET: NetworkPoints/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkPointModel = await _service.GetNetworkPointViewModelAsync(id.Value);
            if (networkPointModel == null)
            {
                return NotFound();
            }
            return View(networkPointModel);
        }

        // POST: NetworkPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, NetworkPointViewModel networkPointModel)
        {
            if (id != networkPointModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateNetworkPointAsync(networkPointModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetworkPointExists(networkPointModel.Id))
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
            return View(networkPointModel);
        }

        // GET: NetworkPoints/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var networkPointModel = await _service.GetNetworkPointViewModelAsync(id.Value);
            if (networkPointModel == null)
            {
                return NotFound();
            }

            return View(networkPointModel);
        }

        // POST: NetworkPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteNetworkPointAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool NetworkPointExists(Guid id)
        {
            return _service.IsExist(id);
        }
    }
}
